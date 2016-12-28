using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DYV.Models
{
    public class MessagePurchase
    {
        public int Id { get; set; }
        public PurchaseType PurchaseType { get; set; }
        public int NumberPurchased { get; set; }
        public bool DoExpire { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int NumberUsed { get; set; }

        public int MessagesRemaining()
        {
            return NumberPurchased - NumberUsed;
        }
    }
}
