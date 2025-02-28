using Application.Db;
using Application.Handlers;
using Application.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;

namespace Application;

internal class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Configuration.AddUserSecrets<Program>();
        builder.Services.AddSingleton<ITelegramBotClient, TelegramBotClient>(provider =>
        {
            var token = builder.Configuration["TelegramBot:Token"];
            return new TelegramBotClient(token);
        });
        builder.Services.AddHostedService<TelegramBotService>();
        builder.Services.AddDbContext<ScheduleDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddScoped<DbService>();
        builder.Services.AddScoped<TextMessageHandler>();
        builder.Services.AddHttpClient();
        builder.Services.AddScoped<GuapRaspApiService>();
        builder.Build().Run();
    }
}