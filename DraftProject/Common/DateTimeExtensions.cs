using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace DraftProject.Common
{
    public static class DateTimeExtensions
    {
        public static DateTime PersianToMiladiDateTime(this DateTime date)
        {

            return
                new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, new PersianCalendar());
        }

        public static DateTime PersianToMiladiDate(this DateTime date)
        {
            return
                new DateTime(date.Year, date.Month, date.Day, new PersianCalendar());
        }

        public static string MiladiToStringPersianDateTime(this DateTime date)
        {
            var pc = new PersianCalendar();
            return
                $"{pc.GetYear(date):00}/{pc.GetMonth(date):00}/{pc.GetDayOfMonth(date):00} {date.Hour:00}:{date.Minute:00}:{date.Second:00}";
        }

        public static string MiladiToStringPersianDate(this DateTime date)
        {
            var pc = new PersianCalendar();
            return
                $"{pc.GetYear(date):00}/{pc.GetMonth(date):00}/{pc.GetDayOfMonth(date):00}";
        }

        public static string CovertToName(this DateTime date)
        {
            var pc = new PersianCalendar();
            return
                $"{pc.GetYear(date):00}-{pc.GetMonth(date):00}-{pc.GetDayOfMonth(date):00} {date.Hour:00}.{date.Minute:00}.{date.Second:00}";
        }
        //public static DateTime ToMiladiDate(this DateTime date)
        //{
        //    return
        //        new DateTime(date.Year, date.Month, date.Day, new PersianCalendar());
        //}
        //public static string ToStringTime(this DateTime date)
        //{
        //    return
        //        $"{date.Hour:00}:{date.Minute:00}:{date.Second:00}";
        //}

    }
}
