using DYV.Models.SubscriberDashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DYV.Models.User
{
    public class SubscriberUser : ApplicationUser
    {
        public List<SubscriberPractice> SubscriberPractices { get; set; }
        public List<DayList> DayLists { get; set; }
    }
}
