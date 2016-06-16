using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using Zopa.Helpers;
using Zopa.Models;

namespace Zopa.Tests.Helpers
{
    public class CSVReaderTests
    {
        [TestClass]
        public class ReadOffer
        {
            [TestMethod]
            public void Should_Read_Every_Offer_Until_End_Of_Stream()
            {
                var stream = new StringReader(
                  @"Bob,0.075,640
                    Jane,0.069,480
                    Fred,0.071,520
                    Mary,0.104,170
                    John,0.081,320
                    Dave,0.074,140
                    Angela,0.071,60");

                var reader = new CSVReader(stream);

                var offers = new List<Offer>();

                Offer offer;
                while ((offer = reader.ReadOffer()) != null)
                    offers.Add(offer);

                Assert.AreEqual(7, offers.Count);
            }
        }
    }
}
