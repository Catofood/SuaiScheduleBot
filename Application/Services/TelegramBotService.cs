using Application.Cache;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Application.Services;

public class TelegramBotService : IHostedService
{
    private readonly ITelegramBotClient _botClient;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly CacheService _cacheService;
    private readonly TextMessageHandleService _textMessageHandleService;

    public TelegramBotService(ITelegramBotClient botClient, IServiceScopeFactory scopeFactory, CacheService cacheService, TextMessageHandleService textMessageHandleService)
    {
        _botClient = botClient;
        _scopeFactory = scopeFactory;
        _cacheService = cacheService;
        _textMessageHandleService = textMessageHandleService;
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = Array.Empty<UpdateType>()
        };
        _botClient.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            receiverOptions,
            cancellationToken
        );
        

        await _cacheService.UpdateCache();
        
        var botInfo = await _botClient.GetMe(cancellationToken: cancellationToken);
        Console.WriteLine($"Application {botInfo.Username} is running...");
        Console.ReadLine();

        await StopAsync(cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _cacheService.FlushDb();

        var cts = new CancellationTokenSource();
        await cts.CancelAsync();
    }

    private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update,
        CancellationToken cancellationToken)
    {
        if (update.Message != null) await _textMessageHandleService.HandleMessage(update.Message);
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