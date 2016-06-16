using System;

namespace Zopa.Models
{
    public class MonthlyCompoundResult
    {
        public int Months { get; }
        public int CompoundsPerYears { get; }
        public int Requested { get; }
        public double Rate { get; }

        public double Monthly => Total / Months;

        //Compounding interest rate calculate from: A = P (1 + r/n) ^ nt
        /*
         * A = the future value of the investment/loan, including interest 
         * P = the principal investment amount (the initial deposit or loan amount)
         * r = the anual interest rate (decimal) 
         * n = the number of times that interest is compounded per year 
         * t = the number of years the money is invested or borrowed for 
         */
        public double Total =>
            Requested * (Math.Pow(1 + (Rate / CompoundsPerYears), Months));

        public MonthlyCompoundResult(int requested, int months, double rate)
        {
            Requested = requested;
            Months = months;
            Rate = rate;
            CompoundsPerYears = 12; // Monthly compounding
        }
    }
}
