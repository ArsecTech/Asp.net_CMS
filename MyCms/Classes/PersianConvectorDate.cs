using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace MyCms
{
    public static class PersianConvectorDate
    {
        public static string ToShamsi(this DateTime value)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(value) + "/" + pc.GetMonth(value).ToString("00") + "/" + pc.GetDayOfMonth(value).ToString("00");

        }
    }
}