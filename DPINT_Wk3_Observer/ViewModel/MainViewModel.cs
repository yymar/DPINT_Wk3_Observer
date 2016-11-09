using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using DPINT_Wk3_Observer.Model;
using Microsoft.Practices.ServiceLocation;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace DPINT_Wk3_Observer.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        #region Properties to bind to
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged("Name"); }
        }

        private int? _flightNumberIncoming;
        public int? FlightNumberIncoming
        {
            get { return _flightNumberIncoming; }
            set { _flightNumberIncoming = value; RaisePropertyChanged("FlightNumberIncoming"); }
        }

        private string _from;
        public string From
        {
            get { return _from; }
            set { _from = value; RaisePropertyChanged("From"); }
        }

        private int? _flightNumberBelt;
        public int? FlightNumberBelt
        {
            get { return _flightNumberBelt; }
            set { _flightNumberBelt = value; RaisePropertyChanged("FlightNumberBelt"); }
        }

        private int? _beltNumber;
        public int? BeltNumber
        {
            get { return _beltNumber; }
            set { _beltNumber = value; RaisePropertyChanged("BeltNumber"); }
        }

        private int? _flightNumberBeltClear;
        public int? FlightNumberBeltClear
        {
            get { return _flightNumberBeltClear; }
            set { _flightNumberBeltClear = value; RaisePropertyChanged("FlightNumberBeltClear"); }
        }

        public RelayCommand AddIncomingCommand { get; set; }
        public RelayCommand AssignBeltCommand { get; set; }
        public RelayCommand ClearBeltCommand { get; set; }
        public RelayCommand OpenWindowCommand { get; set; }

        #endregion Properties to bind to

        private BaggageHandler _baggageHandler;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(BaggageHandler handler)
        {
            _baggageHandler = handler;
            OpenWindowCommand = new RelayCommand(OpenWindow);
            AddIncomingCommand = new RelayCommand(AddIncoming);
            AssignBeltCommand = new RelayCommand(AssignBelt);
            ClearBeltCommand = new RelayCommand(ClearBelt);
        }

        private void OpenWindow()
        {
            ArrivalsMonitor window = new ArrivalsMonitor();
            window.Show();

            var locator = ServiceLocator.Current.GetInstance<ViewModelLocator>();
            locator.ArrivalsList.Last().Name = this.Name;
            Name = "";

            locator.ArrivalsList.Last().UpdateDestinations(_baggageHandler.BaggageDestinations);
        }

        private void AddIncoming()
        {
            if (FlightNumberIncoming.HasValue && !String.IsNullOrWhiteSpace(From))
            {
                _baggageHandler.IncomingFlight(FlightNumberIncoming.Value, From);
                FlightNumberIncoming = null;
                From = null;

                UpdateArrivalsViewModels();
            }
        }

        private void AssignBelt()
        {
            if (FlightNumberBelt.HasValue && BeltNumber.HasValue)
            {
                _baggageHandler.AssignBelt(FlightNumberBelt.Value, BeltNumber.Value);
                FlightNumberBelt = null;
                BeltNumber = null;

                UpdateArrivalsViewModels();
            }
        }

        private void ClearBelt()
        {
            if (FlightNumberBeltClear.HasValue)
            {
                // TODO: Implement the feature to clear belts.
                // TIP: BaggageHandler has a function for this.

                MessageBox.Show("This function is not implemented yet");
            }
        }

        private void UpdateArrivalsViewModels()
        {
            var arrivals = ServiceLocator.Current.GetInstance<ViewModelLocator>().ArrivalsList;
            foreach (var arrivalViewModel in arrivals)
            {
                arrivalViewModel.UpdateDestinations(_baggageHandler.BaggageDestinations);
            }
        }
    }
}