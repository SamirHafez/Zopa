using System;
using System.IO;
using Zopa.Helpers;
using Zopa.Models;

namespace Zopa
{
    class Program
    {
        static void Main(string[] args)
        {
            var filePath = args[0];
            var requested = int.Parse(args[1]);

            try
            {
                MonthlyCompoundResult result;
                using (StreamReader sr = new StreamReader(filePath))
                {
                    var csvReader = new CSVReader(sr);
                    var calculator = new Calculator(csvReader);
                    result = calculator.Execute(requested, months: 36);
                }

                Print(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void Print(MonthlyCompoundResult result)
        {
            Console.WriteLine($"Requested amount: £{result.Requested}");
            Console.WriteLine($"Rate: {result.Rate * 100:F1}%");
            Console.WriteLine($"Monthly repayment: £{result.Monthly:F2}");
            Console.WriteLine($"Total repayment: £{result.Total:F2}");
        }
    }
}
