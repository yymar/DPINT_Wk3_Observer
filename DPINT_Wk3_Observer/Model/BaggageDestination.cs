using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPINT_Wk3_Observer.Model
{
    public class BaggageDestination
    {
        private int _flightNumber;
        public int FlightNumber
        {
            get { return _flightNumber; }
            set
            {
                _flightNumber = value;
            }
        }

        private string _from;
        public string From
        {
            get { return _from; }
            set
            {
                _from = value;
            }
        }

        private int? _belt;
        public int? Belt
        {
            get { return _belt; }
            set
            {
                _belt = value;
            }
        }

        public BaggageDestination(int flightNo, string from, int? belt)
        {
            FlightNumber = flightNo;
            From = from;
            Belt = belt;
        }
    }
}
