using System;
using System.Collections.Generic;
using System.Linq;

namespace Gas
{
   public partial class LegacyCalculator
   {
      public IPlannedStart Calculate(List<DateTime> dates, int requiredDays = 1)
      {
         dates.Sort((a, b) => a.CompareTo(b));

         var plannedStart = new PlannedStart { StartTime = DateTime.MinValue, Count = 0 };

         // check if dates no items then return early
         if (dates.Count == 0)
         {
            return plannedStart;
         }

         var requiredNumberInFirstWeek = requiredDays;

         var startOfFirstWeek = dates[0];
         // add a week
         var startOfSecondWeek = startOfFirstWeek.AddMilliseconds(7 * 24 * 60 * 60 * 1000);

         var countsForFirstWeek = dates
            .Where(x => x > startOfFirstWeek && x < startOfFirstWeek.AddMilliseconds(7 * 24 * 60 * 60 * 1000)) // add a week
            .Count()
            ;

         var countsForSecondWeek = dates
            .Where(x => x > startOfSecondWeek && x < startOfSecondWeek.AddMilliseconds(7 * 24 * 60 * 60 * 1000)) // add a week
            .Count()
            ;

         if (countsForSecondWeek > countsForFirstWeek && countsForSecondWeek >= requiredNumberInFirstWeek)
         {
            plannedStart = new PlannedStart { StartTime = startOfSecondWeek, Count = countsForSecondWeek };
         }
         return plannedStart;
      }
   }
}
