using ClassLibrary;
using Telegram.Bot.Types;

namespace Bot;

internal static class BotMessageHandler
{
    internal static async Task<string> HandleTextMessage(Message message)
    {
        var messageText = message.Text;
        var chatId = message.Chat.Id;  // Получаем ID чата
        Console.WriteLine($"Received a message from {message.Chat.FirstName} {message.Chat.Id}: {messageText}");

        // Простейшая логика обработки команд
        string response;
        if (messageText.ToLower().Equals("/forceupdate"))
        {
            if (message.Chat.Id.ToString().Equals("427905464"))
            {
                var schedules = await ScheduleManager.ForceUpdateSchedule();
                response = "Done. Amount of classes is: "+ schedules.Count.ToString();
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
        return response;
    }
}