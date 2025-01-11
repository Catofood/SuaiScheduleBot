using ClassLibrary;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Console = System.Console;

namespace Bot;
class Program
{
    private static ScheduleManager _scheduleManager = new ScheduleManager();
    public static async Task Main()
    {
        string token = Token.GetToken();
        var botClient = new TelegramBotClient(token);

        using var cts = new CancellationTokenSource();
        var cancellationToken = cts.Token;

        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = Array.Empty<UpdateType>()
        };
		
        await _scheduleManager.ForceUpdateSchedule();
		
        botClient.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            receiverOptions,
            cancellationToken
        );

        var botInfo = await botClient.GetMe();
        Console.WriteLine($"Bot {botInfo.Username} is running...");
		
        Console.ReadLine();
		
        cts.Cancel();
    }

    public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.Message == null) return;
        var chatId = update.Message.Chat.Id;
        var messageText = update.Message.Text;
        var firstName = update.Message.Chat.FirstName;
        Console.WriteLine($"Received a message from {firstName} {update.Message.Chat.Id}: {messageText}");
        string response = "I don't understand that command.";
        if (messageText.ToLower().Equals("/forceupdate"))
        {
            if (update.Message.Chat.Id.ToString().Equals("427905464"))
            {
                response = "Done. Amount of classes is: " + _scheduleManager.Schedule.Count.ToString();
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
        Console.WriteLine("Sending message to {0} {1}: {2}", firstName, chatId, response);
        await botClient.SendMessage(chatId, response, cancellationToken: cancellationToken);
    }

    public static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        var errorMessage = exception switch
        {
            ApiRequestException apiEx => $"Telegram API Error:\n[{apiEx.ErrorCode}]\n{apiEx.Message}",
            _ => exception.ToString()
        };

        Console.WriteLine(errorMessage);
        return Task.CompletedTask;
    }
}