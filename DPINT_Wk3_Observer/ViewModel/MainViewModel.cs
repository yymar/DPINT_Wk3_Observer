using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using DPINT_Wk3_Observer.Model;
using Microsoft.Practices.ServiceLocation;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Collections.ObjectModel;

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
        private string _nieuweVluchtVanaf;
        public string NieuweVluchtVanaf
        {
            get { return _nieuweVluchtVanaf; }
            set { _nieuweVluchtVanaf = value; RaisePropertyChanged("NieuweVluchtVanaf"); }
        }

        private int _nieuweVluchtAantalKoffers;
        public int NieuweVluchtAantalKoffers
        {
            get { return _nieuweVluchtAantalKoffers; }
            set { _nieuweVluchtAantalKoffers = value; RaisePropertyChanged("NieuweVluchtAantalKoffers"); }
        }

        public VluchtInformatieViewModel Band1 { get; set; }
        public VluchtInformatieViewModel Band2 { get; set; }
        public VluchtInformatieViewModel Band3 { get; set; }
        public RelayCommand NieuweVluchtCommand { get; set; }
        public RelayCommand AssignVluchtenCommand { get; set; }
        public RelayCommand VerversBaggagebandenCommand { get; set; }

        public ObservableCollection<VluchtInformatieViewModel> WachtendeVluchten { get; set; }
        #endregion Properties to bind to

        private Aankomsthal _aankomsthal;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(Aankomsthal aankomsthal)
        {
            NieuweVluchtCommand = new RelayCommand(AddNieuweVlucht);
            AssignVluchtenCommand = new RelayCommand(AssignVluchten);
            VerversBaggagebandenCommand = new RelayCommand(VerversBaggagebanden);
            WachtendeVluchten = new ObservableCollection<VluchtInformatieViewModel>();

            NieuweVluchtAantalKoffers = 50;

            _aankomsthal = aankomsthal;

            Band1 = new VluchtInformatieViewModel();
            Band2 = new VluchtInformatieViewModel();
            Band3 = new VluchtInformatieViewModel();

            InitializeDefaultVluchten();
            VerversWachtendeVluchten();
        }

        private void InitializeDefaultVluchten()
        {
            _aankomsthal.NieuweInkomendeVlucht("New York", 70);
            _aankomsthal.NieuweInkomendeVlucht("Paris", 23);
            _aankomsthal.NieuweInkomendeVlucht("Beijing", 84);
            _aankomsthal.NieuweInkomendeVlucht("London", 65);
            _aankomsthal.NieuweInkomendeVlucht("Barcelona", 45);
            _aankomsthal.NieuweInkomendeVlucht("Sydney", 92);
            _aankomsthal.NieuweInkomendeVlucht("Moskow", 14);
            _aankomsthal.NieuweInkomendeVlucht("Rio de Janeiro", 98);
            _aankomsthal.NieuweInkomendeVlucht("Cape Town", 73);
            _aankomsthal.NieuweInkomendeVlucht("Tokyo", 38);
        }
        
        private void AssignVluchten()
        {
            _aankomsthal.WachtendeVluchtenNaarBand();
            VerversWachtendeVluchten();
            VerversBaggagebanden();
        }

        private void VerversWachtendeVluchten()
        {
            WachtendeVluchten.Clear();
            foreach (var vlucht in _aankomsthal.WachtendeVluchten)
            {
                WachtendeVluchten.Add(new VluchtInformatieViewModel()
                {
                    AantalKoffers = vlucht.AantalKoffers,
                    VertrokkenVanuit = vlucht.VertrokkenVanuit
                });
            }
        }

        private void VerversBaggagebanden()
        {
            Band1.Update(_aankomsthal.Baggagebanden[0].HuidigeVlucht);
            Band2.Update(_aankomsthal.Baggagebanden[1].HuidigeVlucht);
            Band3.Update(_aankomsthal.Baggagebanden[2].HuidigeVlucht);
        }

        private void AddNieuweVlucht()
        {
            if (!String.IsNullOrWhiteSpace(NieuweVluchtVanaf))
            {
                _aankomsthal.NieuweInkomendeVlucht(NieuweVluchtVanaf, NieuweVluchtAantalKoffers);
                
                WachtendeVluchten.Add(new VluchtInformatieViewModel()
                {
                    AantalKoffers = NieuweVluchtAantalKoffers,
                    VertrokkenVanuit = NieuweVluchtVanaf
                });

                NieuweVluchtAantalKoffers = 50;
                NieuweVluchtVanaf = null;
            }
        }
    }
}