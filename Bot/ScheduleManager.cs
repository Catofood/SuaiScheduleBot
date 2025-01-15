using System.Text;
using System.Xml.Linq;
using Bot;
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
    public List<ScheduleItem>? Schedule;
    
    public async Task<List<ScheduleItem>> GetScheduleFromDb(string groupName)
    {
        await using var db = new ScheduleDbContext();
        List<ScheduleItem> scheduleItems = db.Schedule
            .Where(item => item.GroupNames.Contains(groupName))
            .Where(item => item.IsWeekOdd == true)
            .ToList();
        return scheduleItems;
    }
    private async Task SendScheduleToDb()
    {
        await using var db = new ScheduleDbContext();
        Console.WriteLine("Clearing database...");
        db.Database.ExecuteSqlRaw("DELETE FROM \"Schedule\"");
        Console.WriteLine("Adding schedule in database...");
        foreach (var scheduleItem in Schedule)
        {
            db.Add(scheduleItem);            
        }
        await db.SaveChangesAsync();
        Console.WriteLine("Schedule added in database.");
    }
    private static async Task<List<ScheduleItem>> ParseScheduleAsync(string filePath)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        var studySchedules = new List<ScheduleItem>();
        using (var streamReader = new StreamReader(filePath, Encoding.GetEncoding("windows-1251")))
        {
            var doc = await Task.Run(() => XDocument.Load(streamReader));
            var studies = doc.Descendants("rasp").Elements("study");
            foreach (var study in studies)
                if (study != null)
                {
                    var studyScheduleItem = new ScheduleItem();

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
                    studyScheduleItem.GroupNames = study.Attribute("group")?.Value.Split(";").ToList();
                    studyScheduleItem.Teacher = study.Attribute("prep")?.Value;
                    studyScheduleItem.Department = study.Attribute("dept")?.Value;

                    studySchedules.Add(studyScheduleItem);
                }

            return studySchedules;
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