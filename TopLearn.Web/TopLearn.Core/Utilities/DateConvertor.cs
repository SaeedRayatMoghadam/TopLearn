using System.Globalization;

namespace TopLearn.Core.Utilities;

public static class DateConvertor
{
    public static string ToShamsi(this DateTime date)
    {
        var pc = new PersianCalendar();

        return pc.GetYear(date) + "/" + pc.GetMonth(date).ToString("00") + "/" + pc.GetDayOfMonth(date).ToString("00");
    }
}