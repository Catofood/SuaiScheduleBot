using Application.Cache;
using Application.Client;
using Application.Client.DTO;
using Application.Mapping;
using Application.Storage;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;
using User = Application.Storage.Entities.User;

namespace Application.Services;

public class TextMessageHandleService
{
    private readonly ITelegramBotClient _botClient;
    private readonly CacheService _cacheService;
    private readonly ScheduleDbContext _scheduleDbContext;
    private readonly SuaiClient _suaiClient;
    private readonly IMapper _mapper;
    private readonly AsyncMapperService _asyncMapperService;
    private readonly TextGenerationService _textGenerationService;

    public TextMessageHandleService(ITelegramBotClient botClient, CacheService cacheService, ScheduleDbContext scheduleDbContext, SuaiClient suaiClient, IMapper mapper, AsyncMapperService asyncMapperService, TextGenerationService textGenerationService)
    {
        _botClient = botClient;
        _cacheService = cacheService;
        _scheduleDbContext = scheduleDbContext;
        _suaiClient = suaiClient;
        _mapper = mapper;
        _asyncMapperService = asyncMapperService;
        _textGenerationService = textGenerationService;
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
                    await _cacheService.UpdateCache();
                    await _botClient.SendMessage(userTelegramId, "Done");
                }
                else
                {
                    await _botClient.SendMessage(userTelegramId, "This command is only available for admins");
                }
            }
            else
            {
                var groupId = await _cacheService.GetGroupId(userMessageText);
                if (groupId == null)
                {
                    await _botClient.SendMessage(userTelegramId, "Группа с таким названием не найдена");
                }
                else
                {
                    var weeklyScheduleDto = await _suaiClient.GetGroupStudyEvents(groupId.GetValueOrDefault(), DateTimeOffset.Now.LocalDateTime, DateTimeOffset.MaxValue);
                    var events = _mapper.Map<List<EventDto>>(weeklyScheduleDto);
                    var pairs = (await _asyncMapperService
                        .ConvertEventDtosToPairs(events))
                        .Where(x => x.PairStartTime.HasValue)
                        .OrderBy(x => x.PairStartTime)
                        .GroupBy(x => x.PairStartTime!.Value.DayOfYear) // Берем первые 7 предстоящих учебных дней
                        .Take(7) 
                        .SelectMany(x => x)
                        .ToList(); 
                    var textResponses = await _textGenerationService.Generate(pairs);
                    foreach (var text in textResponses)
                    {
                        await _botClient.SendMessage(userTelegramId, text);
                    }
                }
            }
    }
}