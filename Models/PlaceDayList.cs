using DYV.Models.SubscriberDashboard;
using DYV.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DYV.Models
{
    public class PlaceDayList
    {
        public int PlaceId { get; set; }
        public Place Place { get; set; }
        public int DayListId { get; set; }
        public DayList DayList { get; set; }
        public int ZoomLevel { get; set; }
    }
}
