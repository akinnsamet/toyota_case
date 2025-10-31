using System.Globalization;
using System.Runtime.CompilerServices;
using Toyota.Shared.Entities.Enum;

namespace Toyota.Shared.Extensions
{
    /// <summary>
    ///     Value type extension methods
    /// </summary>
    public static class DateExtensions
    {
        public static Func<DateTime, int> WeekProjector = d => CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
               d,
               CalendarWeekRule.FirstFourDayWeek,
               DayOfWeek.Sunday);

        public static int WeekOfDate(this DateTime time)
        {
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        public static DateTime WeekLastMoment(this DateTime date)
        {
            int weekCounter = 0;
            while (date.AddDays(weekCounter).DayOfWeek != DayOfWeek.Sunday)
            {
                weekCounter++;
            }

            return new DateTime(date.AddDays(weekCounter).Year, date.AddDays(weekCounter).Month, date.AddDays(weekCounter).Day, 23, 59, 59);
        }

        public static DateTime WeekFirstMoment(this DateTime date)
        {
            int weekCounter = 0;
            while (date.AddDays(weekCounter).DayOfWeek != DayOfWeek.Monday)
            {
                weekCounter--;
            }
            return new DateTime(date.AddDays(weekCounter).Year, date.AddDays(weekCounter).Month, date.AddDays(weekCounter).Day, 0, 0, 0);
        }

        public static DateTime FirstMondayNextWeek(this DateTime date)
        {
            int weekCounter = 1;
            while (date.AddDays(weekCounter).DayOfWeek != DayOfWeek.Monday)
            {
                weekCounter++;
            }
            return new DateTime(date.AddDays(weekCounter).Year, date.AddDays(weekCounter).Month, date.AddDays(weekCounter).Day, 0, 0, 0);
        }

        public static DateTime DayFirstMoment(this DateTime? date)
        {
            var tempdate = date ?? DateTime.UtcNow;
            return new DateTime(tempdate.Year, tempdate.Month, tempdate.Day, 0, 0, 0);
        }
        public static DateTime DayLastMoment(this DateTime? date)
        {
            var tempdate = date ?? DateTime.UtcNow;
            return new DateTime(tempdate.Year, tempdate.Month, tempdate.Day, 23, 59, 59);
        }

        public static DateTime MonthFirstMoment(this DateTime date)
        {
            var curDate = date.AddDays(-date.Day + 1);
            return new DateTime(curDate.Year, curDate.Month, curDate.Day, 0, 0, 0);
        }
        public static DateTime MonthLastMoment(this DateTime date)
        {
            var lastDayOfMonth = DateTime.DaysInMonth(date.Year, date.Month);
            return new DateTime(date.Year, date.Month, lastDayOfMonth, 23, 59, 59);
        }

        public static DateTime YearFirstMoment(this DateTime date)
        {
            return new DateTime(date.Year, 1, 1, 0, 0, 0);
        }

        public static DateTime YearLastMoment(this DateTime date)
        {
            return new DateTime(date.Year, 12, 31, 23, 59, 59);
        }
        public static int GetAge(DateTime dateOfBirth)
        {
            var today = DateTime.Today;

            var a = (today.Year * 100 + today.Month) * 100 + today.Day;
            var b = (dateOfBirth.Year * 100 + dateOfBirth.Month) * 100 + dateOfBirth.Day;

            return (a - b) / 10000;
        }

        public static DateTime CalculateDate(Enums.TimeTypeEnum timeType, int addTimeValue, DateTime? startTime)
        {
            DateTime returnDate = DateTime.UtcNow;
            if (startTime != null)
                returnDate = startTime.Value;

            switch (timeType)
            {
                case Enums.TimeTypeEnum.Day:
                    returnDate = returnDate.AddDays(addTimeValue);
                    break;
                case Enums.TimeTypeEnum.Week:
                    returnDate = returnDate.AddDays(addTimeValue * 7);
                    break;
                case Enums.TimeTypeEnum.Month:
                    returnDate = returnDate.AddMonths(addTimeValue);
                    break;
                case Enums.TimeTypeEnum.Year:
                    returnDate = returnDate.AddYears(addTimeValue);
                    break;
                default:
                    break;
            }
            return returnDate;
        }
        public static string MaskPhoneNumber(string phoneNumber)
        {
            // Determine start index for mask
            int start = (phoneNumber.Length - 4) / 2;
            // Determine end index for mask
            int end = start + 4;

            // Mask the portion of the phone number
            return phoneNumber.Substring(0, start) + new string('*', 4) + phoneNumber.Substring(end);
        }
        public static (DateTime startDate, DateTime endDate) GetWeekRangeUtil(DateTime inputDate)
        {
            // Haftanın gününü al (0 Pazar, 1 Pazartesi vb.)
            int day = (int)inputDate.DayOfWeek;

            // Haftanın başlangıcını hesapla (Pazartesi)
            DateTime startOfWeek = inputDate.AddDays(-((day + 6) % 7));

            // Haftanın sonunu hesapla (Pazar)
            DateTime endOfWeek = startOfWeek.AddDays(6);

            // Tarihleri YYYY-MM-DD formatında döndür
            //string FormatDate(DateTime date) => date.ToString("yyyy-MM-dd");

            //return (FormatDate(startOfWeek), FormatDate(endOfWeek));
            return (startOfWeek, endOfWeek);
        }
        public static int DiffDays(DateTime date1, DateTime date2)
        {
            TimeSpan diffTime = date1 - date2;
            int diffDays = (int)Math.Ceiling(diffTime.TotalDays);

            return diffDays;
        }
        public static int DiffAbsDays(DateTime date1, DateTime date2)
        {
            // İki tarih arasındaki farkı mutlak değer olarak al
            TimeSpan diffTime = (date1 - date2).Duration();

            // Farkı gün cinsine çevir ve yukarı yuvarla
            int diffAbsDays = (int)Math.Ceiling(diffTime.TotalDays);

            return diffAbsDays;
        }
    }
}
