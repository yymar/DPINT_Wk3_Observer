using DPINT_Wk3_Observer.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPINT_Wk3_Observer.ViewModel
{
    public class BaggageDestinationViewModel : ViewModelBase
    {
        private int _flightNumber;
        public int FlightNumber
        {
            get { return _flightNumber; }
            set { _flightNumber = value; RaisePropertyChanged("FlightNumber"); }
        }

        private string _from;
        public string From
        {
            get { return _from; }
            set { _from = value; RaisePropertyChanged("From"); }
        }

        private int? _belt;
        public int? Belt
        {
            get { return _belt; }
            set { _belt = value; RaisePropertyChanged("Belt"); }
        }
        
        public BaggageDestinationViewModel(BaggageDestination destination)
        {
            Belt = destination.Belt;
            From = destination.From;
            FlightNumber = destination.FlightNumber;
        }
    }
}
