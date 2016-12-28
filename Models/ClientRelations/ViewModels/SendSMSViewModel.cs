using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DYV.Models.ClientRelations
{
    public class SendSMSViewModel
    {
        public IEnumerable<string> Numbers { get; set; }
        public string Message { get; set; }
        public string From { get; set; }
    }
}
