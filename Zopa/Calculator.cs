using System;
using System.Collections.Generic;
using System.Linq;
using Zopa.Helpers;
using Zopa.Models;

namespace Zopa
{
    public class Calculator
    {
        readonly CSVReader Reader;
        IList<Offer> offers;

        public Calculator(CSVReader reader)
        {
            Reader = reader;
        }

        public MonthlyCompoundResult Execute(int requested, int months)
        {
            if (requested < 1000 || requested > 15000 || requested % 100 != 0)
                throw new ArgumentOutOfRangeException(
                    nameof(requested),
                    requested,
                    "{0} must be between [1000,15000] and within intervals of £100.");

            if (offers == null)
                offers = LoadOffers();

            var orderedOffers = offers
                .OrderBy(o => o.Rate)
                .ThenByDescending(o => o.Available);

            var rate = CalculateRate(requested, orderedOffers);
            return new MonthlyCompoundResult(requested, months, rate);
        }

        private static double CalculateRate(int requested, IOrderedEnumerable<Offer> orderedOffers)
        {
            int initialRequested = requested;
            double minBorrow = 0;
            foreach (var offer in orderedOffers)
            {
                var requiredAvailable = Math.Min(requested, offer.Available);
                minBorrow += offer.Rate * requiredAvailable;

                if ((requested -= requiredAvailable) == 0)
                    return minBorrow / initialRequested;
            }

            throw new Exception("Cannot satisfy request at current time.");
        }

        private IList<Offer> LoadOffers()
        {
            var offers = new List<Offer>();
            Offer currentOffer;
            while ((currentOffer = Reader.ReadOffer()) != null)
                offers.Add(currentOffer);

            return offers;
        }
    }
}
