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
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddSingleton<ITelegramBotClient, TelegramBotClient>(provider =>
        {
            // var token = Token.GetToken();
            // return new TelegramBotClient(token);
            return new TelegramBotClient(builder.Configuration["TelegramBot:Token"]);
        });
        builder.Services.AddHostedService<TelegramBotService>();
        builder.Services.AddDbContext<ScheduleDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddScoped<DbService>();
        builder.Services.AddScoped<TextMessageHandler>();
        builder.Build().Run();
    }
}