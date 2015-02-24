using CurrencyConvertor.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CurrencyConvertor.Data
{
    public class DataPersister
    {
        public static IEnumerable<CurrencyDetailedViewModel> GetDetailedCurrencies(string currenciesDocumentPath)
        {
            var currenciesDocumentRoot = XDocument.Load(currenciesDocumentPath).Root;

            var currenciesVMs = from currencyElement in currenciesDocumentRoot.Elements("currency")
                                select new CurrencyDetailedViewModel()
                                {
                                    Name = currencyElement.Element("name").Value,
                                    Bank = currencyElement.Element("bank").Value,
                                    Banknotes = currencyElement.Element("banknotes").Value,
                                    Coins = currencyElement.Element("coins").Value,
                                    MainUser = currencyElement.Element("user").Value,
                                    ImagePath = currencyElement.Element("flag").Value,
                                    Iso = currencyElement.Element("iso").Value
                                };
            return currenciesVMs;
        }

        public static IEnumerable<CurrencySimpleViewModel> GetCurrenciesNamesAndIso(string currenciesDocumentPath)
        {
            var currenciesDocumentRoot = XDocument.Load(currenciesDocumentPath).Root;

            var currenciesVMs = from currencyElement in currenciesDocumentRoot.Elements("currency")
                                select new CurrencySimpleViewModel()
                                {
                                    Name = currencyElement.Element("name").Value,
                                    Iso = currencyElement.Element("iso").Value,
                                    ImagePath = currencyElement.Element("flag").Value
                                };
            return currenciesVMs;
        }
    }
}
