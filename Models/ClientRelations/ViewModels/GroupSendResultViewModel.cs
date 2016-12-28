using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DYV.Models.ClientRelations
{
    public class GroupSendResultViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Date Sent")]
        [DataType(DataType.Date)]
        public DateTime DateSent { get; set; }

        [Display(Name = "Number of Recipients")]
        public int TotalRequested { get; set; }

        [Display(Name ="Delivered Messages")]
        public int TotalSucceeded { get; set; }

        [Display(Name = "Failed Delivery")]
        public int TotalFailed { get; set; }

        [Display(Name = "Already Registered")]
        public int TotalExisting { get; set; }

        public string Error { get; set; }

        [Display(Name = "Sent By")]
        public string SenderName { get; set; }
    }
}
