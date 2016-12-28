using DYV.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DYV.Models.ClientRelations
{
    public class SMSGroupSendResult : MessageGroupResult
    {
        public List<SMSSendResult> SendResults { get; set; } = new List<SMSSendResult>();
    }
}
