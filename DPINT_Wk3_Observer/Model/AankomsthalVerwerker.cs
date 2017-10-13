using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPINT_Wk3_Observer.Model
{
    public class AankomsthalVerwerker
    {
        private Queue<Vlucht> _wachtendeVluchten;
        public IEnumerable<Vlucht> WachtendeVluchten { get { return _wachtendeVluchten; } }

        private List<BaggageBand> _baggagebanden;
        public IList<BaggageBand> Baggagebanden { get { return _baggagebanden; } }

        public AankomsthalVerwerker()
        {
            _wachtendeVluchten = new Queue<Vlucht>();
            _baggagebanden = new List<BaggageBand>();

            _baggagebanden.Add(new BaggageBand("Band 1", 60));
            _baggagebanden.Add(new BaggageBand("Band 2", 90));
            _baggagebanden.Add(new BaggageBand("Band 3", 120));
        }

        public void NieuweInkomendeVlucht(string vertrokkenVanuit, int aantalKoffers)
        {
            _wachtendeVluchten.Enqueue(new Vlucht(vertrokkenVanuit, aantalKoffers));
        }

        private bool CanAssignVlucht()
        {
            return _baggagebanden.Any(bb => bb.IsEmpty);
        }

        public void WachtendeVluchtenNaarBand()
        {
            while(CanAssignVlucht() && _wachtendeVluchten.Any())
            {
                BaggageBand legeBand = _baggagebanden.FirstOrDefault(bb => bb.IsEmpty);
                Vlucht volgendeVlucht = _wachtendeVluchten.Dequeue();

                legeBand.HandelNieuweVluchtAf(volgendeVlucht);
            }
        }
    }
}
