using DYV.Models.PlaceViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DYV.Models.SubscriberDashboard
{
    public class CreateDayListViewModel
    {
        public string name { get; set; }
        public DateTime dateAdded { get; set; }
        public List<PlaceSubscriberListItemViewModel> dayListPlaces { get; set; }
    }
}
