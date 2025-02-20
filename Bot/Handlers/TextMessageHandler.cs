using Bot.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Telegram.Bot.Types;
using User = Telegram.Bot.Types.User;

namespace Bot.Handlers;

public class TextMessageHandler : IMessageHandler
{
    private readonly ITelegramBotClient _botClient;
    private readonly IServiceScopeFactory _scopeFactory;

    public TextMessageHandler(ITelegramBotClient botClient, IServiceScopeFactory scopeFactory)
    {
        _botClient = botClient;
        _scopeFactory = scopeFactory;

    }

    public async Task HandleMessage(Message message)
    {
        var chatId = message.Chat.Id;
        var messageText = message.Text.ToLower();
        var firstName = message.Chat.FirstName;
        Console.WriteLine($"Received a message from {firstName} {message.Chat.Id}: {messageText}");
        var response = "";
        bool isAdmin;
        using (var scope = _scopeFactory.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<ScheduleDbContext>();
            
            // Проверка на наличие пользователя в базе данных
            var user = db.Users.FirstOrDefault(u => u.TelegramId == message.Chat.Id);
            if (user == null)
            {
                db.Users.Add(new Db.User() { TelegramId = message.Chat.Id });
                await db.SaveChangesAsync();
            }
            isAdmin = user?.IsAdmin ?? false;
            switch (messageText)
            {
                case "/info":
                    if (isAdmin)
                        response = $"Amount of studies is: {db.Studies.Count().ToString()}\n" +
                                   $"Amount of groups is: {db.Groups.Count().ToString()}\n" +
                                   $"Amount of users is: {db.Users.Count().ToString()}";
                    else
                        response = "You are not allowed to use this command.";
                    break;
                // case "/update":
                //     if (isAdmin)
                //     {
                //         await _scheduleDbContextService.ForceUpdateSchedule();
                //         response = $"Schedule is successfully updated!";
                //
                //     }
                //     else
                //     {
                //         response = "You are not allowed to use this command.";
                //     }
                //     break;
                // case "/schedule":
                //     // foreach (var scheduleItem in await _scheduleManager.GetStudiesFromDb("М411"))
                //     // {
                //     //     response += scheduleItem.GetString();
                //     // }
                //     break;
                case "/start":
                    if (db.Users.Any(user => user.TelegramId == message.Chat.Id) == false)
                    {
                        response = "Welcome to SuaiProject!";
                    }
                    else
                    {
                        response = "Welcome back to SuaiProject!";
                    }

                    break;
                default:
                {
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
                    
                    List<Study> studies = db.Studies
                        .Include(study => study.Groups)
                        .Where(item => item.Groups.Any(group => group.Name == groupName))
                        .Where(item => item.Week == 1)
                        .ToList();
                    foreach (var study in studies)
                        response +=
                            $"Id: {study.Id}\n" +
                            $"Day: {study.Day?.ToString() ?? "N/A"}\n" +
                            $"SchedulePosition: {study.SchedulePosition?.ToString() ?? "N/A"}\n" +
                            $"Week: {study.Week?.ToString() ?? "N/A"}\n" +
                            $"LocationName: {study.Building ?? "N/A"}\n" +
                            $"Classroom: {study.Room ?? "N/A"}\n" +
                            $"LessonName: {study.Discipline ?? "N/A"}\n" +
                            $"TypeOfLesson: {study.Type ?? "N/A"}\n" +
                            $"GroupNames: {study.Groups?.Select(g => g.Name).Aggregate((current, next) => current + ", " + next) ?? "N/A"}\n" +
                            $"Teacher: {study.Teacher ?? "N/A"}\n" +
                            $"Department: {study.Department ?? "N/A"}";
                    await db.SaveChangesAsync();
                    break;
                }
            }

            await db.SaveChangesAsync();
        }
        Console.WriteLine($"Sending message to {firstName} {chatId}: {response}");
        await _botClient.SendMessage(chatId, response);
    }
}