using Bot.Handlers;
using Bot.Db;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Bot.Services;

public class TelegramBotService : IHostedService
{
    private readonly ITelegramBotClient _botClient;
    private readonly TextMessageHandler _textMessageHandler;
    private readonly ScheduleDbContext _scheduleDbContext;

    public TelegramBotService(ITelegramBotClient botClient, TextMessageHandler textMessageHandler,
        ScheduleDbContext scheduleDbContext)
    {
        _botClient = botClient;
        _textMessageHandler = textMessageHandler;
        _scheduleDbContext = scheduleDbContext;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var cts = new CancellationTokenSource();

        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = Array.Empty<UpdateType>()
        };

        // await _dbService.ForceUpdateSchedule();

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

    private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update,
        CancellationToken cancellationToken)
    {
        if (update.Message.Text != null) await _textMessageHandler.Handle(update.Message);
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