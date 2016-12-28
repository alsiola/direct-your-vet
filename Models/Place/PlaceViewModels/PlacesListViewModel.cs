using DYV.Models.PracticeViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DYV.Models.PlaceViewModels
{
    public class PlacesListViewModel<TPlaceViewModel>
    {
        public List<TPlaceViewModel> Places { get; set; }
        public List<PracticeListItemViewModel> Practices { get; set; }
        public int TotalResults { get; set; }
    }
}
