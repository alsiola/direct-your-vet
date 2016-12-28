using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DYV.Models
{
    public class UserInvite
    {
        public int Id { get; set; }
        public int PracticeId { get; set; }
        public DateTime CreationDate { get; set; }
        public string Email { get; set; }
        public string Code { get; set; }
        public bool Used { get; set; }
    }
}
