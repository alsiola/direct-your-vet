using DYV.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DYV.Models.PracticeViewModels
{
    public class PracticeDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SubscriberUser> LinkedAccounts { get; set; }
    }
}
