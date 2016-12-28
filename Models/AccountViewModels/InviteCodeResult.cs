using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DYV.Models.AccountViewModels
{
    public class InviteCodeResult
    {
        public bool Success { get; set; }
        public string Error { get; set; }
        public int PracticeId { get; set; }
    }
}
