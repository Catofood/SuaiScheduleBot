using ClassLibrary;
using Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using User = Db.User;

namespace Bot;

public class TelegramBotService : IHostedService
{
    private readonly TelegramBotClient _botClient;
    private static ScheduleManager _scheduleManager = new ScheduleManager();
    public TelegramBotService()
    {
        var token = Token.GetToken();
        _botClient = new TelegramBotClient(token);
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var cts = new CancellationTokenSource();

        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = Array.Empty<UpdateType>()
        };

        await _scheduleManager.ForceUpdateSchedule();

        _botClient.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            receiverOptions,
            cancellationToken
        );

        var botInfo = await _botClient.GetMe();
        Console.WriteLine($"Bot {botInfo.Username} is running...");

        Console.ReadLine();

        await StopAsync(cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        var cts = new CancellationTokenSource();
        cts.Cancel();
    }

    private static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update,
        CancellationToken cancellationToken)
    {
        if (update.Message == null) return;
        var chatId = update.Message.Chat.Id;
        var messageText = update.Message.Text.ToLower();
        var firstName = update.Message.Chat.FirstName;
        Console.WriteLine($"Received a message from {firstName} {update.Message.Chat.Id}: {messageText}");
        var response = "";
        bool isAdmin;
        await using (var db = new ScheduleDbContext())
        {
            isAdmin = db.Users.FirstOrDefault(u => u.TelegramId == update.Message.Chat.Id)?.IsAdmin ?? false; 
        }
        

        switch (messageText)
        {
            case "/info":
                if (isAdmin)
                {
                    await using var db = new ScheduleDbContext();
                    response = $"Amount of studies is: {db.Studies.Count().ToString()}\n" + 
                               $"Amount of groups is: {db.Groups.Count().ToString()}\n" +
                               $"Amount of users is: {db.Users.Count().ToString()}";
                }
                else
                {
                    response = "You are not allowed to use this command.";
                }

                break;
            case "/update":
                if (isAdmin)
                {
                    await _scheduleManager.ForceUpdateSchedule();
                    response = $"Schedule is successfully updated!";

                }
                else
                {
                    response = "You are not allowed to use this command.";
                }
                break;
            case "/schedule":
                // foreach (var scheduleItem in await _scheduleManager.GetStudiesFromDb("М411"))
                // {
                //     response += scheduleItem.GetString();
                // }
                break;
            case "/start":
                await using (var db = new ScheduleDbContext())
                {
                    if (db.Users.Any(user => user.TelegramId == update.Message.Chat.Id) == false)
                    {
                        db.Users.Add(new User() { TelegramId = update.Message.Chat.Id });
                        response = "Welcome to SuaiProject!";
                        db.SaveChanges();
                    }
                    else response = "Welcome back to SuaiProject!";
                    
                }
                break;
            default:
                // На данный момент, если сообщение пользователя не является командой,
                // то мы принимаем сообщение за поиск расписания по группе.
                // Например, приходит сообщение от пользователя: "М411",
                // в таком случае мы возвращаем пользователю список занятий группы М411.
                var groupName = messageText
                    .Replace('c', 'с')
                    .Replace('s', 'с')
                    .Replace('v', 'в')
                    .Replace('r', 'р')
                    .Replace('m', 'м')
                    .Replace('k', 'к')
                    .Replace('a', 'а')
                    .Replace('e', 'е')
                    .Replace('o', 'о')
                    .Replace('p', 'р')
                    .Replace('x', 'ч')
                    .Replace('b', 'в');
                await using (var db = new ScheduleDbContext())
                {
                    List<Study> studies = db.Studies
                        .Include(study => study.Groups)
                        .Where(item => item.Groups.Any(group => group.Name == groupName))
                        .Where(item => item.Week == 1)
                        .ToList();
                    foreach (var study in studies)
                    {
                        response += $@"
Id: {study.Id}
Day: {study.Day?.ToString() ?? "N/A"}
SchedulePosition: {study.SchedulePosition?.ToString() ?? "N/A"}
Week: {study.Week?.ToString() ?? "N/A"}
LocationName: {study.Building ?? "N/A"}
Classroom: {study.Room ?? "N/A"}
LessonName: {study.Discipline ?? "N/A"}
TypeOfLesson: {study.Type ?? "N/A"}
GroupNames: {study.Groups?.Select(g => g.Name).Aggregate((current, next) => current + ", " + next) ?? "N/A"}
Teacher: {study.Teacher ?? "N/A"}
Department: {study.Department ?? "N/A"}
";
                    }
                }
                break;
        }
        Console.WriteLine($"Sending message to {firstName} {chatId}: {response}");
        await botClient.SendMessage(chatId, response, cancellationToken: cancellationToken);
    }

    private static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception,
        CancellationToken cancellationToken)
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