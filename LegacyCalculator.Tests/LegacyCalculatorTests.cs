using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Gas
{
   public class LegacyCalculatorTests
   {
      [Test]
      public void Empty()
      {
         // arrange
         var calculator = new LegacyCalculator();

         // act
         var result = calculator.Calculate(new List<DateTime>());

         // assert
         Assert.AreEqual(DateTime.MinValue, result.StartTime);
         Assert.AreEqual(0, result.Count);
      }

      [Test]
      public void WithOneDate()
      {
         // arrange
         var calculator = new LegacyCalculator();
         var dates = new List<DateTime> { new DateTime() };

         // act
         var result = calculator.Calculate(dates);

         // assert
         Assert.AreEqual(DateTime.MinValue, result.StartTime);
         Assert.AreEqual(0, result.Count);
      }

      [Test]
      public void WithManySortedDates()
      {
         // arrange
         var calculator = new LegacyCalculator();
         var dates = new List<DateTime> {
            new DateTime(2018, 1, 1),
            new DateTime(2018, 1, 2),
            new DateTime(2018, 1, 10),
            new DateTime(2018, 1, 11),
            new DateTime(2018, 1, 12),
         };

         // act
         var result = calculator.Calculate(dates);

         // assert
         Assert.AreEqual(new DateTime(2018, 1, 8), result.StartTime);
         Assert.AreEqual(3, result.Count);
      }

      [Test]
      public void WithManyUnSortedDates()
      {
         // arrange
         var calculator = new LegacyCalculator();
         var dates = new List<DateTime> {
            new DateTime(2018, 1, 11),
            new DateTime(2018, 1, 10),
            new DateTime(2018, 1, 2),
            new DateTime(2018, 1, 1),
            new DateTime(2018, 1, 12),
         };

         // act
         var result = calculator.Calculate(dates);

         // assert
         Assert.AreEqual(new DateTime(2018, 1, 8), result.StartTime);
         Assert.AreEqual(3, result.Count);
      }

      [Test]
      public void WithDuplicateDates()
      {
         // arrange
         var calculator = new LegacyCalculator();
         var dates = new List<DateTime> {
            new DateTime(2018, 1, 11),
            new DateTime(2018, 1, 10),
            new DateTime(2018, 1, 2),
            new DateTime(2018, 1, 10),// Duplicate date
            new DateTime(2018, 1, 1),
            new DateTime(2018, 1, 12),
            new DateTime(2018, 1, 2)
         };

         // act
         var result = calculator.Calculate(dates);

         // assert
         Assert.AreEqual(new DateTime(2018, 1, 8), result.StartTime);
         Assert.AreEqual(3, result.Count);
      }
   }
}
