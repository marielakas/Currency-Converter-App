using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConvertor.ViewModels
{
    public class CurrencyViewModel
    {
        public string Error { get; set; }

        public string Success { get; set; }

        public string FromCurrency { get; set; }

        public string ToCurrency { get; set; }
    }
}
