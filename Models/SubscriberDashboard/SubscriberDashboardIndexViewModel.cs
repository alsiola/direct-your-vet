using DYV.Models.PlaceViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DYV.Models.SubscriberDashboard
{
    public class SubscriberDashboardIndexViewModel
    {
        public string PracticeName { get; set; }
        public PlacesListViewModel<PlaceSubscriberListItemViewModel> Places { get; set; }
    }
}
