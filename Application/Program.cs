using Application.Cache;
using Application.Client;
using Application.Mapping;
using Application.Services;
using Application.Storage;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Telegram.Bot;

namespace Application;

internal class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Configuration.AddUserSecrets<Program>();
        
        builder.Services.AddTransient<ITelegramBotClient, TelegramBotClient>(provider =>
        {
            var token = builder.Configuration["TelegramBot:Token"];
            return new TelegramBotClient(token);
        });
        
        builder.Services.AddHostedService<TelegramBotService>();
        builder.Services.AddAutoMapper(typeof(DtoToCacheEntitiesProfile));
        builder.Services.AddSingleton<CacheService>();
        builder.Services.AddSingleton<IConnectionMultiplexer>(provider =>
            ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("Redis")));
        builder.Services.AddDbContext<DbContext, ScheduleDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));
        builder.Services.AddHttpClient();
        builder.Services.AddTransient<TextMessageHandleService>();
        builder.Services.AddTransient<SuaiClient>();
        builder.Services.AddTransient<AsyncMapperService>();
        
        builder.Build().Run();
    }
}