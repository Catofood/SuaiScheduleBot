using Application.Client;
using Application.Client.DTO;
using Application.Handlers;
using Microsoft.EntityFrameworkCore;
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
    private readonly TextMessageHandler _textMessageHandler;
    private readonly GuapClient _guapClient;

    public TelegramBotService(ITelegramBotClient botClient, TextMessageHandler textMessageHandler, GuapClient guapClient)
    {
        _botClient = botClient;
        _textMessageHandler = textMessageHandler;
        _guapClient = guapClient;
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

        // TODO: Делаем все нужные запросы на API гуап и сохраняем данные в кэш
        var buildings = await _guapClient.GetBuildings();
        var rooms = await _guapClient.GetRooms();
        var groups = await _guapClient.GetGroups();
        var teachers = await _guapClient.GetTeachers();
        var departments = await _guapClient.GetDepartments();
        var version = await _guapClient.GetVersion();
        
        var botInfo = await _botClient.GetMe(cancellationToken: cancellationToken);
        Console.WriteLine($"Application {botInfo.Username} is running...");
        Console.ReadLine();

        await StopAsync(cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        var cts = new CancellationTokenSource();
        await cts.CancelAsync();
    }

    private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update,
        CancellationToken cancellationToken)
    {
        await _textMessageHandler.HandleMessage(update.Message);
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