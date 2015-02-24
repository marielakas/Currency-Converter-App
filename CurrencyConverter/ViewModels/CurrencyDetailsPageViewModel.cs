using CurrencyConverter.Behavior;
using CurrencyConvertor.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CurrencyConvertor.ViewModels
{
    public class CurrencyDetailsPageViewModel
    {
        public string CurrenciesDetailsPath { get; set; }

        public CurrencyDetailedViewModel CurrentModel { get; set; }

        public string ImagePath { get; set; }

        public CurrencyDetailsPageViewModel()
        {
            this.CurrenciesDetailsPath = "..\\AppX\\ViewModels\\Files\\currencies.xml";
            IEnumerable<CurrencyDetailedViewModel> models =
                DataPersister.GetDetailedCurrencies(this.CurrenciesDetailsPath);
            this.CurrentModel = models.Where(x => x.Iso == Helper.Iso).FirstOrDefault();
        }

        private ICommand download;
        public ICommand Download
        {
            get
            {
                if (this.download == null)
                {
                    this.download = new DelegateCommand<object>(this.HandleDownloadCommand);
                }
                return this.download;
            }
        }

        private async void HandleDownloadCommand(object parameter)
        {
            var savePicker = new Windows.Storage.Pickers.FileSavePicker();

            var plainTextFileTypes = new List<string>(new string[]{".txt", ".doc"});

            savePicker.FileTypeChoices.Add(
                new KeyValuePair<string, IList<string>>("New Document", plainTextFileTypes)
                );

            var saveFile = await savePicker.PickSaveFileAsync();

            if (saveFile != null)
            {
                StringBuilder text = new StringBuilder();
                text.Append("Bank: ");
                text.AppendLine(this.CurrentModel.Bank);
                text.AppendLine();
                text.AppendLine("Coins");
                text.AppendLine();
                text.AppendLine(this.CurrentModel.Coins);
                text.AppendLine("Banknotes");
                text.AppendLine();
                text.AppendLine(this.CurrentModel.Banknotes);
                await Windows.Storage.FileIO.WriteTextAsync(saveFile, text.ToString());
                await new Windows.UI.Popups.MessageDialog("File Saved!").ShowAsync();
            }
        }
    }
}
