using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DYV.Models.PlaceViewModels
{
    public class PlaceListItemViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Place Name")]
        public string Name { get; set; }

        [Display(Name = "Address")]
        public string Address1 { get; set; }

        [Display(Name = "Date Added")]
        public string DateAdded { get; set; }
    }
}
