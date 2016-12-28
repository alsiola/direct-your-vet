using DYV.Models.PlaceViewModels;
using DYV.Services.EFUpdate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DYV.Models.User;

namespace DYV.Models.SubscriberDashboard
{
    public class DayListPlaceViewModel
    {
        public DayListPlaceViewModel(Place place, int zoomLevel, int dayListId, ClientUser clientUser)
        {
            Place = new PlaceDetailsViewModel();
            Place.UpdateProperties(place, true);
            ClientName = clientUser.Name;
            ZoomLevel = zoomLevel;
            DayListId = dayListId;
        }

        public int DayListId { get; set; }
        public int ZoomLevel { get; set; }
        public string ClientName { get; set; }
        public PlaceDetailsViewModel Place { get; set; }
    }
}
