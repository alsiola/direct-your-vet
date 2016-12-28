using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DYV.Models.PlaceViewModels
{
    public class PlaceSubscriberListItemViewModel : PlaceListItemViewModel
    {
        [Display(Name = "Client Name")]
        public string ClientName { get; set; }

        [Display(Name = "Post Code")]
        public string PostCode { get; set; }

        public string City { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
    }
}
