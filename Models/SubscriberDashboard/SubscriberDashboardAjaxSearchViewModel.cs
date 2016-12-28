using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DYV.Models.Ajax
{
    public class SubscriberDashboardAjaxSearchViewModel
    {
        public string ClientNameTerm { get; set; }
        public string PlaceNameTerm { get; set; }
        public string AddressTerm { get; set; }
        public int Page { get; set; }
        public int Take { get; set; }
    }
}
