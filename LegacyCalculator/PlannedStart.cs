using System;

namespace Gas
{
   public partial class LegacyCalculator
   {
      private class PlannedStart : IPlannedStart
      {
         public DateTime StartTime { get; set; }
         public int Count { get; set; }
      }
   }
}
