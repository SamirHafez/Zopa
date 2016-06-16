using System;
using System.IO;
using Zopa.Models;

namespace Zopa.Helpers
{
    public class CSVReader : IDisposable
    {
        TextReader textReader;

        public CSVReader(TextReader stream)
        {
            textReader = stream;
        }

        public Offer ReadOffer()
        {
            var line = textReader.ReadLine();

            if (line == null)
                return null;

            return new Offer(line, delimiter: ',');
        }
        public void Dispose() => textReader.Dispose();
    }
}
