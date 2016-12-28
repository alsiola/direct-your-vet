using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DYV.Models.ClientRelations
{
    public class EmailGroupSendResult : MessageGroupResult
    {
        public List<EmailSendResult> SendResults { get; set; } = new List<EmailSendResult>();
    }
}
