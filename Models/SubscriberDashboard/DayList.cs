using DYV.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DYV.Models.SubscriberDashboard
{
    public class DayList
    {
        public int Id { get; set; }
        public string SubscriberUserId { get; set; }
        public SubscriberUser SubscriberUser { get; set; }

        public string Name { get; set; }
        public DateTime DateAdded { get; set; }
        public ICollection<PlaceDayList> PlaceDayLists { get; set; }

        public int allPlacesMapZoom { get; set; }
    }
}
