using CurrencyConverter.Behavior;
using CurrencyConvertor.Data;
using CurrencyConvertor.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace CurrencyConvertor.ViewModels
{
    public class SearchViewModel : ViewModelBase
    {
        private string currenciesDocumentPath = "..\\AppX\\ViewModels\\Files\\currencies.xml";

        private string queryText;

        public string QueryText
        {
            get
            {
                return this.queryText;
            }

            set
            {
                this.queryText = value;
                this.OnPropertyChanged("QueryText");
                this.LoadResults();
            }
        }

        private ObservableCollection<CurrencySimpleViewModel> results;
        public IEnumerable<CurrencySimpleViewModel> Results
        {
            get
            {
                if (this.results == null)
                {
                    results = new ObservableCollection<CurrencySimpleViewModel>();
                }

                return results;
            }
            set
            {
                this.results.Clear();

                foreach (var item in value)
                {
                    this.results.Add(item);
                }
            }
        }

        private void LoadResults()
        {
            var allNamesAndIso = DataPersister.GetCurrenciesNamesAndIso(this.currenciesDocumentPath);

            foreach (var item in allNamesAndIso)
            {
                if(item.Name.ToLower().Contains(this.QueryText.ToLower()))
                {
                    this.results.Add(item);
                }
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

        public Frame Frame { get; set; }
    }
}
