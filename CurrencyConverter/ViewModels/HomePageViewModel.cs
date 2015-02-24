using CurrencyConverter.Behavior;
using CurrencyConvertor.Data;
using CurrencyConvertor.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace CurrencyConvertor.ViewModels
{
    public class HomePageViewModel:ViewModelBase
    {
        private string CurrenciesDetailsPath { get; set; }

        private IEnumerable<CurrencySimpleViewModel> simpleCurrenciesCollection;

        public IEnumerable<CurrencySimpleViewModel> SimpleCurrenciesCollection
        {
            get
            {
                if (this.simpleCurrenciesCollection == null)
                {
                    this.SimpleCurrenciesCollection = DataPersister.GetCurrenciesNamesAndIso(this.CurrenciesDetailsPath);
                }

                return this.simpleCurrenciesCollection;
            }

            set
            {
                this.simpleCurrenciesCollection = value;
            }
        }

        private ICommand showDetails;
        public ICommand ShowDetails
        {
            get
            {
                if (this.showDetails == null)
                {
                    this.showDetails = new DelegateCommand<object>(this.NavigateToDetailsView);
                }
                return this.showDetails;
            }
        }

        private void NavigateToDetailsView(object parameter)
        {
            var args = parameter as SelectionChangedEventArgs;
            if (args.AddedItems.Count != 0)
            {
                var selected = (args.AddedItems[0] as CurrencySimpleViewModel).Iso;
                Helper.Iso = selected;
                this.Frame.Navigate(typeof(CurrencyDetailedView));
            }

          
        }

        public HomePageViewModel()
        {
            this.CurrenciesDetailsPath = "..\\AppX\\ViewModels\\Files\\currencies.xml";
            
        }

        public Frame Frame { get; set; }
    }
}
