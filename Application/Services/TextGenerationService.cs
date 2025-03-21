using System.Text;
using Application.Client;
using Application.Extensions;
using Application.Models;

namespace Application.Services;


public class TextGenerationService
{
    private readonly SuaiClient _suaiClient;

    public TextGenerationService(SuaiClient suaiClient)
    {
        _suaiClient = suaiClient;
    }
    
    

    public async Task<List<string>> Generate(IEnumerable<Pair> pairs)
    {
        var sortedGroupsByDay = pairs.Where(x => x.PairStartTime != null)
            .Where(x => x.PairStartTime.HasValue) 
            .OrderBy(x => x.PairStartTime)
            .GroupBy(x => x.PairStartTime!.Value.DayOfYear)
            .ToDictionary(x => x.Key, x => x.ToList());

        var dailySchedules = new List<string>();
        var strBuilder = new StringBuilder();
        strBuilder.AppendLine("Расписание на ближайшие несколько учебных дней");
        strBuilder.AppendLine();
        foreach (var group in sortedGroupsByDay)
        {
            string day = group.Value.FirstOrDefault()!.PairStartTime!.Value.ToLocalTime().DayOfWeek.ToStringRussian();
            strBuilder.AppendLine(day);
            foreach (var pair in group.Value)
            {
                var time = pair.PairStartTime!.Value.ToLocalTime();
                strBuilder.AppendLine($"{time.ToString("HH:mm")} - {time.AddHours(1.5).ToString("HH:mm")}");
                strBuilder.AppendLine($"{pair.EventType} - {pair.EventName}");
                strBuilder.AppendLine($"{pair.Room} - {pair.Building} - {pair.DepartmentName}");
                strBuilder.AppendLine($"{pair.TeacherName} {pair.TeacherPost}");
                strBuilder.AppendLine();
            }
            dailySchedules.Add(strBuilder.ToString());
            strBuilder.Clear();
        }

        return dailySchedules;
    }
}
