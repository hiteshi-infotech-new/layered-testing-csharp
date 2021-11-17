using System;
using System.Collections.Generic;
using System.Linq;

namespace Gas
{
   public partial class LegacyCalculator
   {
      public IPlannedStart Calculate(List<DateTime> dates, int requiredDays = 1, bool distinctDate = true)
      {
         var plannedStart = new PlannedStart { StartTime = DateTime.MinValue, Count = 0 };
         // check if dates no items then return early
         if (dates.Count == 0)
         {
            return plannedStart;
         }

         if (distinctDate)
            dates = dates.Select(x => x.Date).Distinct().ToList();//Fetch only distinct dates,remove duplicates
         dates.Sort((a, b) => a.CompareTo(b));

         int requiredNumberInFirstWeek = requiredDays;
         //DateTime startOfFirstWeek = dates[0];
         DateTime startOfFirstWeek = FirstDayOfWeek(dates[0]); //To get exact start of the week date

         // add a week
         DateTime startOfSecondWeek = startOfFirstWeek.AddDays(7);//.AddMilliseconds(7 * 24 * 60 * 60 * 1000);

         int countsForFirstWeek = dates
            .Where(x => x > startOfFirstWeek && x < startOfFirstWeek.AddDays(7)).Count();//.AddMilliseconds(7 * 24 * 60 * 60 * 1000)) // add a week

         int countsForSecondWeek = dates
            .Where(x => x > startOfSecondWeek && x < startOfSecondWeek.AddDays(7)).Count();//.AddMilliseconds(7 * 24 * 60 * 60 * 1000)) // add a week

         if (countsForSecondWeek > countsForFirstWeek && countsForSecondWeek >= requiredNumberInFirstWeek)
         {
            plannedStart = new PlannedStart { StartTime = startOfSecondWeek, Count = countsForSecondWeek };
         }
         return plannedStart;
      }


      private static DateTime FirstDayOfWeek(DateTime dt)
      {
         var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
         var diff = dt.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek;

         if (diff < 0)
         {
            diff += 7;
         }
         return dt.AddDays(-diff).Date;
      }

   }
}
