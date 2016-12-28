using DYV.Models.User;
using System.Collections.Generic;
using System;
using System.Linq;
using DYV.Models.ClientRelations;

namespace DYV.Models
{
    public class Practice
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SignupSlug { get; set; }
        public ICollection<ClientPractices> ClientPractices { get; set; }
        public ICollection<SubscriberPractice> SubscriberPractices { get; set; }
        public List<MessagePurchase> MessagePurchases { get; set; }
        public List<MessageGroupResult> MessageGroupSendResults { get; set; } = new List<MessageGroupResult>();

        public int GetMsgQuota(PurchaseType type, DateTime date)
        {
            return 
                MessagePurchases.Where(mp => mp.PurchaseType == type && (!mp.DoExpire || mp.ExpiryDate.CompareTo(date) > 0)).Select(p => p.MessagesRemaining()).Aggregate((p1, p2) => p1 + p2);
        }
    }
}
