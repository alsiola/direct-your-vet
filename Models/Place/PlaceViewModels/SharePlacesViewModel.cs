using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DYV.Models.PlaceViewModels
{
    public class SharePlacesViewModel
    {
        public List<SelectListItem> Practices { get; set; }
        public string SelectedPracticeId { get; set; }
    }
}
