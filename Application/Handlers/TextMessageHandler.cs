using Application.Cache;
using Application.Client;
using Application.Storage;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;
using User = Application.Storage.Entities.User;

namespace Application.Handlers;

public class TextMessageHandler
{
    private readonly ITelegramBotClient _botClient;
    private readonly GuapClient _guapClient;
    private readonly CacheRepository _cacheRepository;
    private readonly ScheduleDbContext _scheduleDbContext;

    public TextMessageHandler(ITelegramBotClient botClient, GuapClient guapClient, CacheRepository cacheRepository, ScheduleDbContext scheduleDbContext)
    {
        _botClient = botClient;
        _guapClient = guapClient;
        _cacheRepository = cacheRepository;
        _scheduleDbContext = scheduleDbContext;
    }

    public async Task HandleMessage(Message message)
    {
        var userMessageText = message.Text
            .ToLower()
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
        long userTelegramId = message.Chat.Id;
        var userEntity = await _scheduleDbContext.Users.FirstOrDefaultAsync(u => u.TelegramId == message.Chat.Id);
        if (userEntity == null)
        {
            var lol = await _scheduleDbContext.Users.AddAsync(new User(){TelegramId = message.Chat.Id});
            // TODO: Добавить приветствие со стикером гуап
        }
        if (userEntity.IsAdmin)
        {
            
        }
        
        
        
        
        var firstName = message.Chat.FirstName;
        Console.WriteLine($"Received a message from {firstName} {message.Chat.Id}: {userMessageText}");
        var response = await GetText(userMessageText);
        Console.WriteLine($"Sending message to {firstName} {userTelegramId}: {response}");
        await _botClient.SendMessage(userTelegramId, response);
    }

    public async Task<string> GetText(string message)
    {
        return "lol";
    }
}