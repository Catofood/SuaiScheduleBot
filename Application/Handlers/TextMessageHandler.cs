using Application.Cache;
using Application.Client;
using Application.Commands;
using Application.Storage;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Telegram.Bot.Types;
using User = Application.Storage.Entities.User;

namespace Application.Handlers;

public class TextMessageHandler : IRequestHandler<HandleTextMessageCommand>
{
    private readonly ITelegramBotClient _botClient;
    private readonly CacheRepository _cacheRepository;
    private readonly ScheduleDbContext _scheduleDbContext;
    private readonly IMediator _mediator;

    public TextMessageHandler(ITelegramBotClient botClient, CacheRepository cacheRepository, ScheduleDbContext scheduleDbContext, IMediator mediator)
    {
        _botClient = botClient;
        _cacheRepository = cacheRepository;
        _scheduleDbContext = scheduleDbContext;
        _mediator = mediator;
    }

    public async Task HandleMessage(Message message)
    {
        var firstName = message.Chat.FirstName;
        var userMessageText = message.Text.ToLower();
        Console.WriteLine($"Received a message from {firstName} {message.Chat.Id}: {userMessageText}");
        
        long userTelegramId = message.Chat.Id;
        
            var userEntity = await _scheduleDbContext.Users.FirstOrDefaultAsync(u => u.TelegramId == message.Chat.Id);
            if (userEntity == null)
            {
                userEntity = new User { TelegramId = message.Chat.Id };
                await _scheduleDbContext.Users.AddAsync(userEntity);
                await _scheduleDbContext.SaveChangesAsync(); 
            }
            if (userMessageText == "/update")
            {
                if (userEntity.IsAdmin)
                {
                    await _botClient.SendMessage(userTelegramId, "Updating caches...");
                    await _mediator.Send(new UpdateCacheCommand());
                    await _botClient.SendMessage(userTelegramId, "Done.");
                }
                else
                {
                    await _botClient.SendMessage(userTelegramId, "This command is only available for admins.");
                }
            }
            else
            {
                await _botClient.SendMessage(userTelegramId, (await _cacheRepository.GetGroupId(userMessageText)).ToString() ?? string.Empty);
            }
    }

    public async Task Handle(HandleTextMessageCommand request, CancellationToken cancellationToken)
    {
        await HandleMessage(request.Message);
    }
}