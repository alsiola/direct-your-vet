using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DYV.Models.PracticeViewModels
{
    public class PracticeListItemViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }

        [Display(Name = "Number of Clients")]
        public int NumClients { get; set; }

        [Display(Name = "Number of Subscribers")]
        public int NumSubscribers { get; set; }
    }
}
