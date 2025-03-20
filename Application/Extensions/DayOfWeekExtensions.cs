namespace Application.Extensions;

public static class DayOfWeekExtensions
{
    static string ToString(this DayOfWeek dayOfWeek, bool useShortFormat = false)
    {
        if (useShortFormat)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Monday:
                    return "ПН";
                case DayOfWeek.Tuesday:
                    return "ВТ";
                case DayOfWeek.Wednesday:
                    return "СР";
                case DayOfWeek.Thursday:
                    return "ЧТ";
                case DayOfWeek.Friday:
                    return "ПТ";
                case DayOfWeek.Saturday:
                    return "СБ";
                default:
                    return "Вне сетки расписания";
            }
        }
        switch (dayOfWeek)
        {
            case DayOfWeek.Monday:
                return "Понедельник";
            case DayOfWeek.Tuesday:
                return "Вторник";
            case DayOfWeek.Wednesday:
                return "Среда";
            case DayOfWeek.Thursday:
                return "Четверг";
            case DayOfWeek.Friday:
                return "Пятница";
            case DayOfWeek.Saturday:
                return "Суббота";
            default:
                return "Вне сетки расписания";
        }
    }
}