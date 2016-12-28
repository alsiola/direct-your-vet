using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DYV.Models.Purchase.ViewModels
{
    public class PurchaseCostItemViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public decimal Price { get; set; }
    }
}
