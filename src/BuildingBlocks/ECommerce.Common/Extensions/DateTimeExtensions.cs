using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Common.Extensions
{
    public static class DateTimeExtensions
    {
        //start of day
        public static DateTime StartOfDay(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, date.Kind);
        }

        //end of day
        public static DateTime EndOfDay(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999, date.Kind);
        }

        //start of month
        public static DateTime StartOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1, 0, 0, 0, date.Kind);
        }

        //end of month
        public static DateTime EndOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month), 23, 59, 59, 999, date.Kind);
        }

        //start of week
        public static DateTime StartOfWeek(this DateTime date, DayOfWeek startOfWeek = DayOfWeek.Monday)
        {
            var diff = (7 + (date.DayOfWeek - startOfWeek)) % 7;
            return date.AddDays(-1 * diff).Date;
        }

        //end of week
        public static DateTime EndOfWeek(this DateTime date, DayOfWeek startOfWeek = DayOfWeek.Monday)
        {
            return date.StartOfWeek(startOfWeek).AddDays(6).EndOfDay();
        }

        //IsWeekend
        public static bool IsWeekend(this DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
        }

        //IsWorkingDay
        public static bool IsWorkingDay(this DateTime date)
        {
            return !date.IsWeekend();
        }

        //ToIsoString
        public static string ToIsoString(this DateTime date)
        {
            return date.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        }

        //From IsoString
        public static DateTime FromIsoString(this string date)
        {
            return DateTime.Parse(date);
        }

        //ToFriendlyDateString

        public static string ToFriendlyDateString(this DateTime date)
        {
          var now = DateTime.Now;
            var diff = now - date;
            if (diff.TotalMinutes < 1)
            {
                return "Şimdi";
            }
            if (diff.TotalMinutes < 60)
            {
                return $"{diff.Minutes} dakika önce";
            }
            if (diff.TotalHours < 24)
            {
                return $"{diff.Hours} saat önce";
            }
            if (diff.TotalDays < 30)
            {
                return $"{diff.Days} gün önce";
            }

            if (diff.TotalDays < 365)
            {
                return $"{diff.Days / 30} ay önce";
            }
            else
            {
                return $"{diff.Days / 365} yıl önce";

            }
        }
    }
}
