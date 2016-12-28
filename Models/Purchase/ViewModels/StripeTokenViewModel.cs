using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DYV.Models.Purchase.ViewModels
{
    public class StripeTokenViewModel
    {
        public string stripeToken { get; set; }
        public int purchaseType { get; set; }
        public int purchaseQty { get; set; }
    }
}
