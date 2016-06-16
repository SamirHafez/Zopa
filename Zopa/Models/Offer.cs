using System;
using System.Globalization;

namespace Zopa.Models
{
    public enum OfferFieldEnum
    {
        LENDER = 0,
        RATE = 1,
        AVAILABLE = 2
    }

    public class Offer
    {
        public string Lender { get; }
        public double Rate { get; }
        public int Available { get; }

        public Offer(string csvLine, char delimiter = ',')
        {
            var split = csvLine.Split(delimiter);

            if (split.Length != 3)
                throw new ArgumentException("Misformatted", nameof(csvLine));

            try
            {
                Lender = split[(int)OfferFieldEnum.LENDER];
                Rate = double.Parse(split[(int)OfferFieldEnum.RATE], CultureInfo.InvariantCulture);
                Available = int.Parse(split[(int)OfferFieldEnum.AVAILABLE], CultureInfo.InvariantCulture);

                if (Rate <= 0 || Available <= 0)
                    throw new ArgumentException("Misformatted", nameof(csvLine));
            }
            catch(Exception e) 
            when (e is FormatException || e is OverflowException)
            {
                throw new ArgumentException("Misformatted", nameof(csvLine));
            }
        }
    }
}
