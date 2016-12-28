using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DYV.Models.User
{
    public class ClientUser : ApplicationUser
    {
        public ICollection<ClientPractices> PermittedPractices { get; set; }
        public ICollection<Place> Places { get; set; }
    }
}
