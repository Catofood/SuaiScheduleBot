using ClassLibrary;
using Telegram.Bot.Types;

namespace Bot;

[MessageHandlerClass]
public class BotMessageHandler
{
	[MessageHandler("/test")]
	public Task<BotResponse> TestHandler(Update message)
	{
		return Task.FromResult(new BotResponse(){Message = message?.Message?.Text ?? "idi nahui"});
	}
	
	[MessageHandler("/hui")]
	public Task<BotResponse> HuiHandler(Update message)
	{
		return Task.FromResult(new BotResponse(){Message = "idi nahui"});
	}
}
// typeof(BotMessageHandler).
// var messageText = message.Text;
// var chatId = message.Chat.Id;  // Получаем ID чата
// Console.WriteLine($"Received a message from {message.Chat.FirstName} {message.Chat.Id}: {messageText}");

// // Простейшая логика обработки команд
// string response;
// if (messageText.ToLower().Equals("/forceupdate"))
// {
// 	if (message.Chat.Id.ToString().Equals("427905464"))
// 	{
// 		response = "Done. Amount of classes is: "+ schedules.Count.ToString();
// 	}
// 	else
// 	{
// 		response = "You are not allowed to do that.";
// 	}
// }
// else
// {
// 	response = messageText.ToLower() switch
// 	{
// 		"/start" => "Welcome to the bot!",  // Ответ на команду /start
// 		"/help" => "How can I assist you?",  // Ответ на команду /help
// 		"/test" => "Downloading schedule...",
// 		"/yo" => "Yo lil bitch",
// 		_ => "I don't understand that command."  // Ответ на неизвестную команду
// 	};
// }