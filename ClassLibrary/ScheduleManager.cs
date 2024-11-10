using System.Diagnostics;
using System.Text;
using System.Xml.Linq;

namespace ClassLibrary;

//
// WARNING
//
// Hardcoded ASF omggg
//
public static class ScheduleManager
{
    private const string RelativePathToScheduleXml = "Schedules";
    private const string ScheduleFileName = "schedule.xml";
    private const string ScheduleDownloadUrl = "https://guap.ru/rasp/current.xml";

    private static async Task<List<StudyScheduleItem>> ParseScheduleAsync(string filePath)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        var studySchedules = new List<StudyScheduleItem>();
        using (var streamReader = new StreamReader(filePath, Encoding.GetEncoding("windows-1251")))
        {
            var doc = await Task.Run(() => XDocument.Load(streamReader));
            var studies = doc.Descendants("rasp").Elements("study");
            foreach (var study in studies)
                if (study != null)
                {
                    var studyScheduleItem = new StudyScheduleItem();

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
                    studyScheduleItem.GroupName = study.Attribute("group")?.Value;
                    studyScheduleItem.Teacher = study.Attribute("prep")?.Value;
                    studyScheduleItem.Department = study.Attribute("dept")?.Value;

                    studySchedules.Add(studyScheduleItem);
                }

            return studySchedules;
        }
    }

    public static string GetScheduleFilePath()
    {
        return Path.Combine(AppContext.BaseDirectory, RelativePathToScheduleXml, ScheduleFileName);
    }

    public static async Task<List<StudyScheduleItem>> ForceUpdateSchedule()
    {
        var schedulePath = GetScheduleFilePath();
        await FileDownloader.DownloadFileAsync(ScheduleDownloadUrl, schedulePath);
        return await ParseScheduleAsync(schedulePath);
    }
}