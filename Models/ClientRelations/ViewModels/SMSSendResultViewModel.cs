using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DYV.Models.ClientRelations
{
    public class SMSSendResultViewModel : MessageSendResultViewModel
    {
        [Display(Name = "Phone Number")]
        public string Number { get; set; }    

        public int DragonFlyMessageIdentifier { get; set; }

        public override string RenderDestinationString()
        {
            return Number;
        }
    }
}
