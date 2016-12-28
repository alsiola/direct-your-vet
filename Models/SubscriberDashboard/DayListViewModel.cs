using DYV.Models.PlaceViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DYV.Models.SubscriberDashboard
{
    public class DayListViewModel
    {
        public int id { get; set; }

        [Display(Name = "Name")]
        public string name { get; set; }

        [Display(Name = "Date Added")]
        public string dateAdded { get; set; }
        public List<DayListPlaceViewModel> dayListPlaces { get; set; }

        public int allPlacesMapZoom { get; set; }
    }
}
