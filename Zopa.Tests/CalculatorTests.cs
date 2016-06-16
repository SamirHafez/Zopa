using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using Zopa.Helpers;

namespace Zopa.Tests
{
    public class CalculatorTests
    {
        [TestClass]
        public class Execute
        {
            StringReader stream;
            CSVReader reader;

            [TestInitialize]
            public void ForEachTest()
            {
                stream = new StringReader(
                  @"Bob,0.075,640
                    Jane,0.069,480
                    Fred,0.071,520
                    Mary,0.104,170
                    John,0.081,320
                    Dave,0.074,140
                    Angela,0.071,60");

                reader = new CSVReader(stream);
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            public void Request_Below_Min_Should_Fail()
            {
                var calculator = new Calculator(reader);

                var result = calculator.Execute(50, 36);
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            public void Request_Above_Max_Should_Fail()
            {
                var calculator = new Calculator(reader);

                var result = calculator.Execute(500000, 36);
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            public void Request_Out_Of_Step_Should_Fail()
            {
                var calculator = new Calculator(reader);

                var result = calculator.Execute(1001, 36);
            }

            [TestMethod]
            [ExpectedException(typeof(Exception), "Cannot satisfy request at current time.")]
            public void Insuficent_Loan_Amount_Should_Fail()
            {
                var calculator = new Calculator(reader);

                var result = calculator.Execute(15000, 36);
            }

            [TestMethod]
            public void Should_Return_Lowest_Rate_1()
            {
                stream = new StringReader(
                  @"Bob,0.071,1000
                    Angela,0.072,1000");

                reader = new CSVReader(stream);

                var calculator = new Calculator(reader);

                var result1000 = calculator.Execute(1000, 36);
                Assert.AreEqual(0.071, Math.Round(result1000.Rate, 3));
            }

            [TestMethod]
            public void Should_Return_Lowest_Rate_2()
            {
                stream = new StringReader(
                  @"Bob,0.071,900
                    Angela,0.072,1000");

                reader = new CSVReader(stream);

                var calculator = new Calculator(reader);

                var result1000 = calculator.Execute(1000, 36);
                Assert.AreEqual(0.071, Math.Round(result1000.Rate, 3));
            }

            [TestMethod]
            public void Should_Return_Lowest_Rate_3()
            {
                stream = new StringReader(
                  @"Bob,0.071,500
                    Angela,0.072,500");

                reader = new CSVReader(stream);

                var calculator = new Calculator(reader);

                var result1000 = calculator.Execute(1000, 36);
                Assert.AreEqual(0.072, Math.Round(result1000.Rate, 3));
            }

            [TestMethod]
            public void Should_Return_Lowest_Rate_4()
            {
                stream = new StringReader(
                  @"Bob,0.071,50
                    Angela,0.072,1000");

                reader = new CSVReader(stream);

                var calculator = new Calculator(reader);

                var result1000 = calculator.Execute(1000, 36);
                Assert.AreEqual(0.072, Math.Round(result1000.Rate, 3));
            }
        }
    }
}
