using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DYV.Models.User
{
    public class SubscriberUserViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsManager { get; set; }

        public void setManager(bool isManager)
        {
            IsManager = isManager;
        }
    }
}
