using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DYV.Models.SubscriberDashboard
{
    public class DayListCreationResult
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
    }
}
