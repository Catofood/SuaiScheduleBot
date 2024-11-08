using Telegram.Bot;  // Основной клиент для взаимодействия с Telegram API
using Telegram.Bot.Exceptions;  // Для обработки ошибок API
using Telegram.Bot.Polling;  // Для управления получением сообщений (Polling)
using Telegram.Bot.Types;  // Основные типы данных (Update, Message и т.д.)
using Telegram.Bot.Types.Enums;  // Перечисления (например, типы обновлений)

namespace Bot;
class Program
{
    static async Task DownloadSchedule()
    {
        string url = "https://guap.ru/rasp/current.xml";
        string directoryRelativePath = "Schedules";
        string fileName = "schedule.xml";

        string filePath = Path.Combine(AppContext.BaseDirectory, directoryRelativePath, fileName);

        await DownloadFileAsync(url, filePath);
    }

    static async Task DownloadFileAsync(string url, string filePath)
    {
        Console.WriteLine($"Начинаю загрузку файла с {url} {filePath}");
        Directory.CreateDirectory(Path.GetDirectoryName(filePath));

        using HttpClient client = new HttpClient();
        try
        {
            byte[] fileBytes = await client.GetByteArrayAsync(url);
            await System.IO.File.WriteAllBytesAsync(filePath, fileBytes);
            Console.WriteLine($"Файл успешно загружен по пути: {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при загрузке: {ex.Message}");
        }
    }

    static async Task Main()
    {
        // Токен вашего бота, полученный от BotFather
        string token = "";

        // Создаем экземпляр клиента Telegram с вашим токеном
        var botClient = new TelegramBotClient(token);

        // Создаем источник для токена отмены, чтобы остановить бота по требованию
        using var cts = new CancellationTokenSource();
        var cancellationToken = cts.Token;

        // Опции для получения обновлений (получаем все типы обновлений)
        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = Array.Empty<UpdateType>()  // Пустой массив = принимать все обновления
        };

        // Запуск получения обновлений (Polling)
        botClient.StartReceiving(
            HandleUpdateAsync,  // Асинхронная функция для обработки входящих сообщений
            HandleErrorAsync,   // Функция для обработки ошибок
            receiverOptions,    // Опции приема
            cancellationToken   // Токен отмены для остановки процесса
        );

        // Получаем информацию о боте и выводим имя в консоль
        var botInfo = await botClient.GetMeAsync();
        Console.WriteLine($"Bot {botInfo.Username} is running...");

        // Ждем нажатия Enter для завершения работы
        Console.ReadLine();
        cts.Cancel();  // Отменяем получение обновлений и завершаем выполнение
    }

    // Обработчик входящих сообщений и обновлений
    static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        // Проверяем, что пришло сообщение с текстом
        if (update.Message is not { Text: { } messageText }) return;

        var chatId = update.Message.Chat.Id;  // Получаем ID чата
        Console.WriteLine($"Received a message from {update.Message.Chat.FirstName} {update.Message.Chat.Id}: {messageText}");

        // Простейшая логика обработки команд
        string response;
        
        if (messageText.ToLower().Equals("/test"))
        {
            if (update.Message.Chat.Id.ToString().Equals("427905464"))
            {
                await DownloadSchedule();
                response = "ok";
            }
            else
            {
                response = "You are not allowed to do that.";
            }
        }
        else
        {
            response = messageText.ToLower() switch
            {
                "/start" => "Welcome to the bot!",  // Ответ на команду /start
                "/help" => "How can I assist you?",  // Ответ на команду /help
                "/test" => "Downloading schedule...",
                "/yo" => "Yo lil bitch",
                _ => "I don't understand that command."  // Ответ на неизвестную команду
            };
        }
        

        // Отправляем ответное сообщение пользователю
        await botClient.SendTextMessageAsync(chatId, response, cancellationToken: cancellationToken);
    }

    // Обработчик ошибок
    static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        // Формируем сообщение об ошибке в зависимости от типа ошибки
        var errorMessage = exception switch
        {
            ApiRequestException apiEx => $"Telegram API Error:\n[{apiEx.ErrorCode}]\n{apiEx.Message}",
            _ => exception.ToString()  // Для всех других ошибок выводим общее описание
        };

        // Выводим сообщение об ошибке в консоль
        Console.WriteLine(errorMessage);
        return Task.CompletedTask;  // Завершаем задачу
    }
}
