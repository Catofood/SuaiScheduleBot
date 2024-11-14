using ClassLibrary;
using Telegram.Bot; // Основной клиент для взаимодействия с Telegram API
using Telegram.Bot.Exceptions; // Для обработки ошибок API
using Telegram.Bot.Polling; // Для управления получением сообщений (Polling)
using Telegram.Bot.Types; // Основные типы данных (Update, Message и т.д.)
using Telegram.Bot.Types.Enums; // Перечисления (например, типы обновлений)

namespace Bot;
class BotCore
{
    private List<StudyScheduleItem> studyScheduleItems;
    static async Task Main()
    {
        string token = Token.GetToken();
        
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
        var botInfo = await botClient.GetMe();
        Console.WriteLine($"Bot {botInfo.Username} is running...");
        
        // Обновляем расписание
        var studyScheduleItems = ScheduleManager.ForceUpdateSchedule();
        
        // Ждем нажатия Enter для завершения работы
        Console.ReadLine();
        cts.Cancel();  // Отменяем получение обновлений и завершаем выполнение
    }

    // Обработчик входящих сообщений и обновлений
    static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        // Проверяем, что пришло непустое сообщение
        if (update.Message == null) return;
        var chatId = update.Message.Chat.Id;  // Получаем ID чата
        
        // Проверяем наличие текста в пришедшем сообщении
        if (!string.IsNullOrEmpty(update.Message.Text))
        {
            // Формируем ответ пользователю в зависимости от содержания сообщения
            var response = await BotMessageHandler.HandleTextMessage(update.Message, studyScheduleItem);
            // Отправляем пользователю сформированный ответ
            await botClient.SendMessage(chatId, response, cancellationToken: cancellationToken);
        }
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
