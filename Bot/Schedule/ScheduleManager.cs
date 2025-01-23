using System.Text;
using System.Xml.Linq;
using Bot.DB;
using Exception = System.Exception;

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
    public List<Group> Groups;

    public async Task<List<Group>> GetGroupsFromDb()
    {
        throw new NotImplementedException();
    }

    private async Task SendScheduleToDb()
    {
        await using var db = new ScheduleDbContext();
        Console.WriteLine("Clearing database...");
        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();
        Console.WriteLine("Adding schedule in database...");
        foreach (var group in Groups) db.Groups.Add(group);
        await db.SaveChangesAsync();
        Console.WriteLine("Schedule added in database.");
    }

    private static async Task<List<Group>> ParseScheduleAsync(string filePath)
    {
        var groups = new List<Group>();
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        using var streamReader = new StreamReader(filePath, Encoding.GetEncoding("windows-1251"));
        var doc = await Task.Run(() => XDocument.Load(streamReader));
        var XElemGroupGroups = doc
            .Descendants("rasp")
            .Elements("study")
            .GroupBy(n => n.Attribute("group").Value)
            .ToList();
        if (XElemGroupGroups.Any())
            foreach (var XElemGroupGroup in XElemGroupGroups)
            {
                var group = new Group();
                var groupName = XElemGroupGroup.FirstOrDefault().Attribute("group").Value.ToLower();
                groupName = groupName
                    .Replace('c', 'с')
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
                group.Name = groupName;
                var xElemUnscheduledStudies = XElemGroupGroup
                    .Where(item =>
                        item.Attribute("day") == null ||
                        item.Attribute("less") == null)
                    .ToList();
                if (xElemUnscheduledStudies.Any())
                {
                    var unscheduledStudies = new List<UnscheduledStudy>();
                    foreach (var xElemUnscheduledStudy in xElemUnscheduledStudies)
                    {
                        var unscheduledStudy = new UnscheduledStudy();
                        unscheduledStudy.Group = group;
                        unscheduledStudy.GroupId = group.Id;
                        unscheduledStudy.Discipline = xElemUnscheduledStudy.Attribute("disc")!.Value;
                        unscheduledStudy.Type = xElemUnscheduledStudy.Attribute("type")!.Value;
                        unscheduledStudy.Department = xElemUnscheduledStudy.Attribute("dept")!.Value;
                        unscheduledStudies.Add(unscheduledStudy);
                    }

                    group.UnscheduledStudies.AddRange(unscheduledStudies);
                }

                // TODO: Сделать чтобы при парсинге провеляось наличие дня, пары и недели
                // Если пары и недели нет, то unscheduled
                // Если есть, то Scheduled
                // Но бывают пары на дистанте, у которых build="Дистант"
                // Для них нужно сделать чтобы в выводе кабинет имел значение "Не указан"
                var weeklyScheduleGroups = XElemGroupGroup
                    .Where(item =>
                        item.Attribute("day") != null &&
                        item.Attribute("less") != null &&
                        item.Attribute("week") != null &&
                        item.Attribute("build") != null &&
                        item.Attribute("disc") != null &&
                        item.Attribute("type") != null &&
                        item.Attribute("group") != null &&
                        item.Attribute("prep") != null &&
                        item.Attribute("room") != null)
                    .GroupBy(item => item.Attribute("week").Value)
                    .ToList();
                if (weeklyScheduleGroups.Any())
                {
                    var weeklySchedules = new List<WeeklySchedule>();
                    // Берем недели по четности
                    foreach (var weeklyScheduleGroup in weeklyScheduleGroups)
                    {
                        var weeklySchedule = new WeeklySchedule();
                        weeklySchedule.Group = group;
                        weeklySchedule.GroupId = group.Id;
                        weeklySchedule.Week = int.Parse(weeklyScheduleGroup.FirstOrDefault().Attribute("week").Value);

                        var dailyScheduleGroups = weeklyScheduleGroup
                            .GroupBy(item => item.Attribute("day").Value)
                            .ToList();
                        // Берем расписание по дням
                        if (dailyScheduleGroups.Any())
                            foreach (var dailyScheduleGroup in dailyScheduleGroups)
                            {
                                if (dailyScheduleGroup.Any())
                                {
                                    var dailySchedule = new DailySchedule
                                    {
                                        WeeklySchedule = weeklySchedule,
                                        WeeklyScheduleId = weeklySchedule.Id,
                                        DayOfWeek = int.Parse(
                                            dailyScheduleGroup.FirstOrDefault().Attribute("day").Value)
                                    };

                                    // Берем расписание дня
                                    foreach (var studyItem in dailyScheduleGroup)
                                    {
                                        var scheduledStudy = new ScheduledStudy();
                                        scheduledStudy.DailySchedule = dailySchedule;
                                        scheduledStudy.DailyScheduleId = dailySchedule.Id;
                                        scheduledStudy.Discipline = studyItem.Attribute("disc").Value;
                                        scheduledStudy.Type = studyItem.Attribute("type").Value;
                                        scheduledStudy.Building = studyItem.Attribute("build").Value;
                                        scheduledStudy.Room = studyItem.Attribute("room").Value;
                                        scheduledStudy.Teacher = studyItem.Attribute("prep").Value;
                                        scheduledStudy.SchedulePosition =
                                            int.Parse(studyItem.Attribute("less").Value);

                                        dailySchedule.ScheduledStudies.Add(scheduledStudy);
                                    }
                                    weeklySchedule.DailySchedules.Add(dailySchedule);
                                }
                                weeklySchedules.Add(weeklySchedule);
                            }
                    }
                    group.WeeklySchedules.AddRange(weeklySchedules);
                }
                groups.Add(group);
            }

        return groups;
    }

    private string GetScheduleFilePath()
    {
        return Path.Combine(AppContext.BaseDirectory, RelativePathToScheduleXml, ScheduleFileName);
    }

    public async Task ForceUpdateSchedule()
    {
        var schedulePath = GetScheduleFilePath();
        await FileDownloader.DownloadFileAsync(ScheduleDownloadUrl, schedulePath);
        Groups = await ParseScheduleAsync(schedulePath);
        await SendScheduleToDb();
    }
}