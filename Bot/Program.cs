using Bot.Handlers;
using Bot.Services;
using Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;

namespace Bot;

internal class Program
{
    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton<ITelegramBotClient, TelegramBotClient>(provider =>
                {
                    var token = Token.GetToken();
                    return new TelegramBotClient(token);
                });
                services.AddHostedService<TelegramBotService>();
                services.AddDbContext<ScheduleDbContext>();
                services.AddScoped<DbService>();
                services.AddScoped<TextMessageHandler>();
            });
    }

    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }
}