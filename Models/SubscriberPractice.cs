using DYV.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DYV.Models
{
    public class SubscriberPractice
    {
        public string SubscriberUserId { get; set; }
        public SubscriberUser SubscriberUser { get; set; }
        public int PracticeId { get; set; }
        public Practice Practice { get; set; }
        public bool IsManager { get; set; }
    }
}
