using System;

namespace LX.Commons
{
    public class DateTimeHelper
    {
        /// <summary>
        /// 获取当前时间所在周，周n的时间数据（以周一作为起始）
        /// </summary>
        /// <param name="day">周n</param>
        /// <returns></returns>
        public static DateTime GetDayOfThisWeek(DayOfWeek day)
        {
            var reDate = new DateTime();
            DateTime now = DateTime.Now.Date;
            var days = day - now.DayOfWeek + 1;
            if (now.DayOfWeek <= day)
                days = days - 7;
            reDate = now.AddDays(days);
            return reDate;
        }

        /// <summary>
        /// 获取给定时间所在周，周n的数据（以周一作为起始）
        /// </summary>
        /// <param name="dt">给定时间</param>
        /// <param name="day">周n</param>
        /// <returns></returns>
        public static DateTime GetDayOfDateWeek(DateTime dt, DayOfWeek day)
        {
            int count = day - dt.DayOfWeek;
            var reDate = dt.AddDays(count);
            if (count == (int)day)
            {
                var count2 = (int)day - 7;
                reDate = dt.AddDays(count2);
            }
            return reDate;
        }

        public static DateTime GetTimeOfDateMonth(int index)
        {
            var reDate = new DateTime();
            DateTime now = DateTime.Now.Date;
            now = now.AddDays(-now.Day + 1);
            reDate = now.AddMonths(-6 + index);
            return reDate;
        }
        /// <summary>
        /// 获取时长
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static string GetTimespan(DateTime? start, DateTime? end)
        {
            if (start == null || end == null) return "";

            var timespan = end.Value - start.Value;
            var days = timespan.Days;
            var hours = timespan.Hours;
            var minutes = timespan.Minutes;
            var TLSC = days + "天" + hours + "小时" + minutes + "分钟";
            return TLSC;
        }

    }
}
