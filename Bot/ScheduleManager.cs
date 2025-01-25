using System.Text;
using System.Xml.Linq;
using Bot;
using Db;
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
    public List<Study>? Schedule;
    
    public async Task<List<Study>> GetScheduleFromDb(string groupName)
    {
        await using var db = new ScheduleDbContext();
        List<Study> scheduleItems = db.Studies
            .Where(item => item.Groups.Any(group => group.Name == groupName))
            .Where(item => item.Week == 1)
            .ToList();
        return scheduleItems;
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
    private static async Task<List<Study>> ParseScheduleAsync(string filePath)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        var studySchedules = new List<Study>();
        using (var streamReader = new StreamReader(filePath, Encoding.GetEncoding("windows-1251")))
        {
            var doc = await Task.Run(() => XDocument.Load(streamReader));
            var xElementStudies = doc.Descendants("rasp").Elements("study");
            foreach (var xElementStudy in xElementStudies)
                if (xElementStudy != null)
                {
                    var study = new Study();

                    int? dayOfWeekNumber = null;
                    {
                        if (int.TryParse(xElementStudy.Attribute("day")?.Value, out var parsedDayOfWeekNumber))
                            dayOfWeekNumber = parsedDayOfWeekNumber;
                    }
                    study.Day = dayOfWeekNumber;

                    int? schedulePosition = null;
                    {
                        if (int.TryParse(xElementStudy.Attribute("less")?.Value, out var result)) schedulePosition = result;
                    }
                    int? week = null;
                    {
                        if (int.TryParse(xElementStudy.Attribute("week")?.Value, out var result)) week = result;
                    }
                    study.SchedulePosition = schedulePosition;
                    study.Week = week;
                    study.Building = xElementStudy.Attribute("build")?.Value;
                    study.Room = xElementStudy.Attribute("room")?.Value;
                    study.Discipline = xElementStudy.Attribute("disc")?.Value;
                    study.Type = xElementStudy.Attribute("type")?.Value;
                    var groupNames = xElementStudy.Attribute("group")?.Value.ToLower().Split(";").ToList();
                    if (groupNames != null)
                    {
                        study.Groups = new List<Group>();
                        foreach (var name in groupNames)
                        {
                            var group = new Group();
                            group.Name = name;
                            group.Studies = new List<Study>(){study};
                            study.Groups.Add(group);
                        }
                    }
                    study.Teacher = xElementStudy.Attribute("prep")?.Value;
                    study.Department = xElementStudy.Attribute("dept")?.Value;

                    studySchedules.Add(study);
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