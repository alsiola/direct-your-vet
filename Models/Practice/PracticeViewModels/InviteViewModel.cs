using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DYV.Models.PracticeViewModels
{
    public class InviteViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        [Required]
        public string InviteEmail { get; set; }

        public string Message { get; set; }
    }
}
