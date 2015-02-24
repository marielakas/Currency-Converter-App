using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConvertor.Models
{
    [DataContract]
    public class CurrencyModel
    {
        [DataMember(Name="error")]
        public string Error { get; set; }

        [DataMember(Name="icc")]
        public string Success { get; set; }

        [DataMember(Name="lhs")]
        public string FromCurrency { get; set; }

        [DataMember(Name="rhs")]
        public string ToCurrency { get; set; }
    }
}
