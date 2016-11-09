/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:DPINT_Wk3_Observer"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using DPINT_Wk3_Observer.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using System.Collections;
using System.Collections.Generic;

namespace DPINT_Wk3_Observer.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        private List<ArrivalsViewModel> _arrivalsViewModels;

        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            _arrivalsViewModels = new List<ArrivalsViewModel>();
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<ViewModelLocator>(() => this);
            SimpleIoc.Default.Register<MainViewModel>();

            SimpleIoc.Default.Register<BaggageHandler>(() => new BaggageHandler(), true);
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public ArrivalsViewModel Arrivals
        {
            get
            {
                var returnValue = new ArrivalsViewModel();
                _arrivalsViewModels.Add(returnValue);
                return returnValue;
            }
        }

        public IEnumerable<ArrivalsViewModel> ArrivalsList
        {
            get
            {
                return _arrivalsViewModels;
            }
        }
        
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}