using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DYV.Models.Purchase.ViewModels
{
    public class PurchaseCostsViewModel
    {
        public List<PurchaseCostItemViewModel> PurchaseOptions { get; set; }
        public string SelectedPurchaseOption { get; set; }
    }
}
