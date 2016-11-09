using DPINT_Wk3_Observer.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPINT_Wk3_Observer.ViewModel
{
    public class ArrivalsViewModel : ViewModelBase
    {
        public ObservableCollection<BaggageDestinationViewModel> BaggageDestinations { get; set; }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged("Name"); }
        }

        public ArrivalsViewModel()
        {
            BaggageDestinations = new ObservableCollection<BaggageDestinationViewModel>();
        }

        public void UpdateDestinations(IEnumerable<BaggageDestination> updatedDestinations)
        {
            // added / changed
            foreach (var dest in updatedDestinations)
            {
                var viewmodel = this.BaggageDestinations.FirstOrDefault(vm => vm.FlightNumber == dest.FlightNumber);
                if(viewmodel != null)
                {
                    viewmodel.Belt = dest.Belt;
                    viewmodel.From = dest.From;
                } else
                {
                    this.BaggageDestinations.Add(new BaggageDestinationViewModel(dest));
                }
            }

            // removed
            var toRemove = from bi in BaggageDestinations
                           where !updatedDestinations.Any(ui => ui.FlightNumber == bi.FlightNumber)
                           select bi;

            foreach (var remove in toRemove.ToList())
            {
                BaggageDestinations.Remove(remove);
            }

        }
    }
}
