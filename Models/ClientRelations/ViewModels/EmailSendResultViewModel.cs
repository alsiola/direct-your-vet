﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DYV.Models.ClientRelations
{
    public class EmailSendResultViewModel : MessageSendResultViewModel
    {
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        public override string RenderDestinationString()
        {
            return EmailAddress;
        }
    }
}
