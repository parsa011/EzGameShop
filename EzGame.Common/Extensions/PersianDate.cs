using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace EzGame.Common.Extensions
{
    public static class PersianDate
    {
        /// <summary>
        /// یک استرینگ تاریخ شمسی را به معادل میلادی تبدیل میکند
        /// </summary>
        /// <param name="persianDate">تاریخ شمسی</param>
        /// <returns>تاریخ میلادی</returns>
        public static DateTime ToGeorgianDateTime(this string persianDate)
        {
            var year = Convert.ToInt32(persianDate.Substring(0, 4));
            var month = Convert.ToInt32(persianDate.Substring(5, 2));
            var day = Convert.ToInt32(persianDate.Substring(8, 2));
            return new DateTime(year, month, day, new PersianCalendar());
        }

        /// <summary>
        /// یک تاریخ میلادی را به معادل فارسی آن تبدیل میکند
        /// </summary>
        /// <param name="georgianDate">تاریخ میلادی</param>
        /// <returns>تاریخ شمسی</returns>
        public static string ToPersianDateString(this DateTime georgianDate)
        {
            var persianCalendar = new PersianCalendar();

            var year = persianCalendar.GetYear(georgianDate).ToString();
            var month = persianCalendar.GetMonth(georgianDate).ToString().PadLeft(2, '0');
            var day = persianCalendar.GetDayOfMonth(georgianDate).ToString().PadLeft(2, '0');
            return $"{year}/{month}/{day}";
        }

        /// <summary>
        /// یک تعداد روز را از یک تاریخ شمسی کم میکند یا به آن آضافه میکند
        /// </summary>
        /// <param name="persianDate">تاریخ شمسی اول</param>
        /// <param name="days">تعداد روزی که میخواهیم اضافه یا کم کنیم</param>
        /// <returns>تاریخ شمسی به اضافه تعداد روز</returns>
        public static string AddDaysToShamsiDate(this string persianDate, int days)
        {
            var dt = persianDate.ToGeorgianDateTime();
            dt = dt.AddDays(days);
            return dt.ToPersianDateString();
        }
    }
}
