using CurrencyConverter.Behavior;
using CurrencyConvertor.Data;
using CurrencyConvertor.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CurrencyConvertor.ViewModels
{
    public class CurrencyConvertorViewModel: ViewModelBase
    {
        private string BaseServiceUrl = "http://www.google.com/ig/calculator?hl=en&q=";

        private string CurrenciesDetailsPath { get; set; }

        private object fromCurrency;
        public object FromCurrency
        {
            get
            {
                return this.fromCurrency;
            }
            set
            {
                if (this.fromCurrency != value)
                {
                    var model = value as CurrencySimpleViewModel;
                    value = model.Iso.ToString();
                    this.fromCurrency = value;
                    this.OnPropertyChanged("FromCurrency");
                }
            }
        }

        private object toCurrency;
        public object ToCurrency
        {
            get
            {
                return this.toCurrency;
            }
            set
            {
                if (this.toCurrency != value)
                {
                    var model = value as CurrencySimpleViewModel;
                    value = model.Iso.ToString();
                    this.toCurrency = value;
                    this.OnPropertyChanged("ToCurrency");
                }
            }
        }

        private ICommand convert;

        public ICommand Convert
        {
            get
            {
                if (this.convert == null)
                {
                    this.convert = new DelegateCommand(this.ExecuteConvert);
                }
                return this.convert;
            }
        }

        private void ExecuteConvert(object parameter)
        {
            this.GetResult();
        }

        private IEnumerable<CurrencySimpleViewModel> currenciesNames;

        public IEnumerable<CurrencySimpleViewModel> CurrenciesNames
        {
            get
            {
                if (this.currenciesNames == null)
                {
                    this.CurrenciesNames = DataPersister.GetCurrenciesNamesAndIso(this.CurrenciesDetailsPath);
                }

                return this.currenciesNames;
            }

            set
            {
                this.currenciesNames = value;
            }
        }

        private double amount = 0;

        public double Amount
        {
            get
            {
                return this.amount;
            }
            set
            {
                if (this.amount != value)
                {
                    this.amount = value;
                    this.OnPropertyChanged("Amount");
                }
            }
        }

        private CurrencyModel result;

        public string Result { get; set; }

        protected async void GetResult()
        {
            StringBuilder url = new StringBuilder();
            url.Append(this.BaseServiceUrl);
            url.Append(this.Amount.ToString());
            url.Append(this.FromCurrency.ToString().ToLower());
            url.Append("=?");
            url.Append(this.ToCurrency.ToString().ToLower());
            var test = await HttpRequester.Get<CurrencyModel>(url.ToString());
            this.Result = test.ToCurrency.ToString();
            this.OnPropertyChanged("Result");
        }

        public CurrencyConvertorViewModel()
        {
            this.CurrenciesDetailsPath = "..\\AppX\\ViewModels\\Files\\currencies.xml";
        }
    }
}
