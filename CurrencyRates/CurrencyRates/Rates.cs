using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyRates
{
    class Rates
    {
        public string PairRate { get; private set; }
        public double Bid { get; private set; }
        public double Ask { get; private set; }
        public double High { get; private set; }
        public double Low { get; private set; }
        public int Direction { get; private set; }
        public string Last { get; private set; }

        public Rates(string pairrate, double bid, double ask, double high, double low, int direction, string last)
        {
            PairRate = pairrate;
            Bid = bid;
            Ask = ask;
            High = high;
            Low = low;
            Direction = direction;
            Last = last;
        }

        public override string ToString()
        {
            return PairRate;
        }
    }
}
