using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DYV.Models.Purchase
{
    public class PurchaseCost
    {
        public int Id { get; set; }
        public PurchaseType PurchaseType { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
