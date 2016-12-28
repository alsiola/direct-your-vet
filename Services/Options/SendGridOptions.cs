using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DYV.Services.Options
{
    public class SendGridOptions
    {
        public string SendGridUser { get; set; }
        public string SendGridPass { get; set; }
        public string SendGridApiKey { get; set; }
    }
}
