using Bot.Db;
using Bot.Handlers;
using Bot.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;

namespace Bot;

internal class Program
{
    public static void Main(string[] args)
    {
        // IHostBuilder host = Host.CreateDefaultBuilder(args)
        //     .ConfigureAppConfiguration((context, config) =>
        //     {
        //         config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        //     })
        //     .ConfigureServices((hostContext, services) =>
        //     {
        //         services.AddSingleton<ITelegramBotClient, TelegramBotClient>(provider =>
        //         {
        //             var token = Token.GetToken();
        //             return new TelegramBotClient(token);
        //         });
        //         services.AddHostedService<TelegramBotService>();
        //         services.AddDbContext<ScheduleDbContext>(options => 
        //             options.UseNpgsql(hostContext.Configuration.GetConnectionString("DbConnectionString")));
        //         services.AddScoped<DbService>();
        //         services.AddScoped<TextMessageHandler>();
        //     });
        // host.Build().Run();
        //


        // var settings = new HostApplicationBuilderSettings
        // {
        //     ContentRootPath = Directory.GetCurrentDirectory(),
        // };
        var builder = WebApplication.CreateBuilder(args);

        // почему-то без этой злоебучей конструкции не находит appsettings.json
        // var projectPath = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.FullName;
        // ArgumentNullException.ThrowIfNull(projectPath);
        // builder.Configuration.SetBasePath(projectPath);
        // builder.Configuration.AddJsonFile("appsettings.json", false, true);
        // TODO: Настроить appsettings.json
        builder.Services.AddSingleton<ITelegramBotClient, TelegramBotClient>(provider =>
        {
            // var token = Token.GetToken();
            // return new TelegramBotClient(token);
            return new TelegramBotClient(builder.Configuration.GetRequiredSection("TelegramBot")
                .GetRequiredSection("Token").Value);
        });
        builder.Services.AddHostedService<TelegramBotService>();
        builder.Services.AddDbContext<ScheduleDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("SuaiProject")));
        builder.Services.AddScoped<DbService>();
        builder.Services.AddScoped<TextMessageHandler>();
        builder.Build().Run();
    }
}