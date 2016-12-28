using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DYV.Models.ClientRelations
{
    public class EmailSendResult : MessageSendResult, IMessageSendResult<EmailGroupSendResult>
    {
        public string EmailAddress { get; set; }
        public int GroupSendResultId { get; set; }
        public EmailGroupSendResult GroupSendResult { get; set; }
    }
}
