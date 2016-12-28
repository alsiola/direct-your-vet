using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DYV.Models.ClientRelations
{
    public class SMSSendResult : MessageSendResult, IMessageSendResult<SMSGroupSendResult>
    {
        public string Number { get; set; }
        public int GroupSendResultId { get; set; }
        public SMSGroupSendResult GroupSendResult { get; set; }
        public int RemainingSMS { get; set; }
        public int DragonFlyMessageIdentifier { get; set; }
    }
}
