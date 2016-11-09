using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPINT_Wk3_Observer.Model
{
    public class BaggageHandler
    {
        private List<BaggageDestination> _baggageDestinations;

        public IEnumerable<BaggageDestination> BaggageDestinations { get { return _baggageDestinations; } }

        public BaggageHandler()
        {
            _baggageDestinations = new List<BaggageDestination>();
        }

        // Called to indicate all baggage is now unloaded.
        public void IncomingFlight(int flightNo, string from)
        {
            var destination = new BaggageDestination(flightNo, from, null);
            _baggageDestinations.Add(destination);
        }

        public void AssignBelt(int flightNo, int beltNo)
        {
            var destination = _baggageDestinations.FirstOrDefault(i => i.FlightNumber == flightNo);
            if(destination != null)
            {
                destination.Belt = beltNo;
            }
        }

        public void ClearBelt(int flightNo)
        {
            var destination = _baggageDestinations.FirstOrDefault(i => i.FlightNumber == flightNo);
            if (destination != null) { 
                // TODO: Straks ook de destination Disposen (dan worden de juiste seintjes gegeven)
                _baggageDestinations.Remove(destination);
            }
        }
    }
}
