using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Zopa.Models;

namespace Zopa.Tests.Models
{
    public class MonthlyCompoundResultTests
    {
        [TestClass]
        public class Total
        {
            [TestMethod]
            public void Compound_Formula_Should_Yeld_Correct_Total_And_Monthly()
            {
                var requested = 1000;
                var months = 36;
                var rate = 0.07;
                var compoundsPerYear = 12;

                var expectedTotal = requested * (Math.Pow(1 + (rate / compoundsPerYear), months));
                var expectedMonthly = expectedTotal / months;

                var mcr = new MonthlyCompoundResult(requested, months, rate);

                var total = mcr.Total;
                var monthly = mcr.Monthly;

                Assert.AreEqual(expectedTotal, total);
                Assert.AreEqual(expectedMonthly, monthly);
            }
        }
    }
}
