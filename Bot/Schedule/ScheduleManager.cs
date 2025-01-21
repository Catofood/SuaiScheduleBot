using System.Text;
using System.Xml.Linq;
using Bot;
using Bot.DB;
using Microsoft.EntityFrameworkCore;

namespace ClassLibrary;

//
// WARNING
//
// Hardcoded ASF omggg
//
public class ScheduleManager
{
    private const string RelativePathToScheduleXml = "Schedules";
    private const string ScheduleFileName = "schedule.xml";
    private const string ScheduleDownloadUrl = "https://guap.ru/rasp/current.xml";
    public List<StudyScheduled>? Schedule;
    // TODO:
    // Переделать парсер под отправку на БД
    // Сделать вывод данных в чат по запросу
    // 
    public async Task<StudyDoubleWeekSchedule> GetScheduleItemsFromDb(string? groupName)
    {
        if (groupName == null)
        {
            return null;
        }
        else
        {
            groupName = groupName?.ToLower();
            // Замена латинских символов на похожие кириллические
            // В базе данных используются только кириллические
            groupName = groupName.Replace('c', 'с')
                .Replace('s', 'с')
                .Replace('v', 'в')
                .Replace('r', 'р')
                .Replace('m', 'м')
                .Replace('k', 'к')
                .Replace('a', 'а')
                .Replace('e', 'е')
                .Replace('o', 'о')
                .Replace('p', 'р')
                .Replace('x', 'ч')
                .Replace('b', 'в');

            await using var db = new ScheduleDbContext();
            // Получаем scheduleItems
            List<StudyScheduledDb>? scheduleItems = db.Schedule
                .Where(item => item.GroupNames.Contains(groupName))
                .OrderBy(item => item.DayOfWeekNumber)
                .ThenBy(item => item.LessonNumber)
                .ToList();
            // Получаем oneDaySchedules
            
            return scheduleItems;
        }
    }
    private async Task SendScheduleToDb()
    {
        await using var db = new ScheduleDbContext();
        Console.WriteLine("Clearing database...");
        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();
        Console.WriteLine("Adding schedule in database...");
        foreach (var scheduleItem in Schedule)
        {
            db.Add(scheduleItem);            
        }
        await db.SaveChangesAsync();
        Console.WriteLine("Schedule added in database.");
    }
    private static async Task<List<StudyScheduledDb>> ParseScheduleAsync(string filePath)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        var schedules = new List<StudyScheduledDb>();
        using (var streamReader = new StreamReader(filePath, Encoding.GetEncoding("windows-1251")))
        {
            var doc = await Task.Run(() => XDocument.Load(streamReader));
            var studies = doc.Descendants("rasp").Elements("study");
            foreach (var study in studies)
                if (study != null)
                {
                    var studyScheduleItem = new StudyScheduledDb();

                    int? dayOfWeekNumber = null;
                    {
                        if (int.TryParse(study.Attribute("day")?.Value, out var parsedDayOfWeekNumber))
                            dayOfWeekNumber = parsedDayOfWeekNumber;
                    }
                    studyScheduleItem.DayOfWeekNumber = dayOfWeekNumber;

                    int? lessonNumber = null;
                    {
                        if (int.TryParse(study.Attribute("less")?.Value, out var result)) lessonNumber = result;
                    }
                    studyScheduleItem.LessonNumber = lessonNumber;
                    bool? isWeekOdd = null;
                    {
                        if (int.TryParse(study.Attribute("week")?.Value, out var value)) isWeekOdd = value % 2 == 1;
                    }
                    studyScheduleItem.IsWeekOdd = isWeekOdd;
                    studyScheduleItem.LocationName = study.Attribute("build")?.Value;
                    studyScheduleItem.Classroom = study.Attribute("room")?.Value;
                    studyScheduleItem.LessonName = study.Attribute("disc")?.Value;
                    studyScheduleItem.TypeOfLesson = study.Attribute("type")?.Value;
                    studyScheduleItem.GroupNames = study.Attribute("group")?.Value.ToLower().Split(";").ToList();
                    studyScheduleItem.Teacher = study.Attribute("prep")?.Value;
                    studyScheduleItem.Department = study.Attribute("dept")?.Value;

                    schedules.Add(studyScheduleItem);
                }

            return schedules;
        }
    }

    private string GetScheduleFilePath()
    {
        return Path.Combine(AppContext.BaseDirectory, RelativePathToScheduleXml, ScheduleFileName);
    }

    public async Task ForceUpdateSchedule()
    {
        var schedulePath = GetScheduleFilePath();
        await FileDownloader.DownloadFileAsync(ScheduleDownloadUrl, schedulePath);
        Schedule = await ParseScheduleAsync(schedulePath);
        await SendScheduleToDb();
    }
}