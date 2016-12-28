using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DYV.Models.SubscriberDashboard
{
    public class SaveZoomLevelViewModel
    {
        public int allPlacesMapZoom { get; set; }
        public IEnumerable<PlaceDayList> placeZooms { get; set; }
    }
}
