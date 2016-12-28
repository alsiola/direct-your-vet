using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DYV.Models.ClientRelations
{
    public interface IMessageSendResultViewModel
    {
        [Display(Name = "Message Sent Successfully?")]
        bool Success { get; set; }

        [Display(Name = "Message Sending Error")]
        string Error { get; set; }

        [Display(Name = "Client Opened Link?")]
        bool SlugOpened { get; set; }

        [Display(Name = "Client Signed Up?")]
        bool RecipientSignedUp { get; set; }

        string RenderDestinationString();
    }

    public abstract class MessageSendResultViewModel : IMessageSendResultViewModel
    {
        public bool Success { get; set; }

        public string Error { get; set; }

        public bool SlugOpened { get; set; } = false;

        public bool RecipientSignedUp { get; set; } = false;

        public abstract string RenderDestinationString();
    }


}
