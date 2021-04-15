using MD.PersianDateTime.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookEnd.Clasess
{
    public class ConvertDateTime
    {
        public DateTime ConvertShamsiToMiladi(string date)
        {
            PersianDateTime persianDateTime1 = PersianDateTime.Parse(date);
            return persianDateTime1.ToDateTime();
        }

        public string ConvertMiladiToShamsi(DateTime Date)
        {
            PersianDateTime persianDateTime = new PersianDateTime(Date);
            return persianDateTime.ToString("yyyy/MM/dd");
        }
    }
}
