using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
using Zopa.Models;

namespace Zopa.Tests.Models
{
    public class OfferTests
    {
        [TestClass]
        public class Ctor
        {
            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void Wrong_Length_CSVLine_Should_Fail()
            {
                var csvLine = "Bob,0.075,640,14";

                var offer = new Offer(csvLine);
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void Wrong_Format_Should_Fail()
            {
                var csvLine = "Bob,0.075,640#1";

                var offer = new Offer(csvLine);
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void Negative_Amount_Should_Fail()
            {
                var csvLine = "Bob,0.075,-640";

                var offer = new Offer(csvLine);
            }

            [TestMethod]
            public void Correct_Input_Should_Produce_Offer()
            {
                var lender = "Bob";
                var rate = "0.075";
                var available = "640";
                var csvLine = $"{lender},{rate},{available}";

                var offer = new Offer(csvLine);

                Assert.AreEqual(lender, offer.Lender.ToString());
                Assert.AreEqual(rate, offer.Rate.ToString(CultureInfo.InvariantCulture));
                Assert.AreEqual(available, offer.Available.ToString());
            }
        }
    }
}
