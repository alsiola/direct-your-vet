using DYV.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DYV.Models.ClientRelations
{
    public abstract class MessageGroupResult
    {
        public MessageGroupResult()
        {
            DateSent = DateTime.Now;
        }

        public int Id { get; set; }

        public int PracticeId { get; set; }
        public Practice Practice { get; set; }

        public DateTime DateSent { get; set; }
        public int TotalRequested { get; set; }
        public int TotalSucceeded { get; set; }
        public int TotalFailed { get; set; }
        public int TotalExisting { get; set; }

        public string Error { get; set; }
        public string SubscriberUserId { get; set; }
        public SubscriberUser SubscriberUser { get; set; }
    }
}
