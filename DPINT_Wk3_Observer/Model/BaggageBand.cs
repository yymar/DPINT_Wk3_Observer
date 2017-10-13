using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DPINT_Wk3_Observer.Model
{
    public class BaggageBand
    {
        public string Naam { get; set; }
        private int _aantalKoffersPerMinuut;
        public Vlucht HuidigeVlucht { get; private set; }
        private Timer _huidigeVluchtTimer;

        public BaggageBand(string naam, int aantalKoffersPerMinuut)
        {
            Naam = naam;
            _aantalKoffersPerMinuut = aantalKoffersPerMinuut;
        }

        public void HandelNieuweVluchtAf(Vlucht vlucht)
        {
            HuidigeVlucht = vlucht;

            if(_huidigeVluchtTimer != null)
            {
                _huidigeVluchtTimer.Stop();
            }

            _huidigeVluchtTimer = new Timer();
            _huidigeVluchtTimer.Interval = (int)((60.0 / _aantalKoffersPerMinuut) * 1000);
            _huidigeVluchtTimer.Tick += KofferVanBandGehaald;

            _huidigeVluchtTimer.Start();
        }

        private void KofferVanBandGehaald(object sender, EventArgs e)
        {
            HuidigeVlucht.AantalKoffers--;

            if(HuidigeVlucht.AantalKoffers == 0)
            {
                _huidigeVluchtTimer.Stop();
            }
        }

        public bool IsEmpty
        {
            get { return HuidigeVlucht == null || HuidigeVlucht.AantalKoffers == 0; }
        }
    }
}
