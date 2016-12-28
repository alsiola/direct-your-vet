using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DYV.Models.Purchase
{
    public class PaymentResult
    {
        public PaymentResult(bool succ)
        {
            Success = succ;
            Errors = new List<string>();
        }

        public PaymentResult(bool succ, string error)
        {
            Success = succ;
            Errors = new List<string>();
            Errors.Add(error);
        }

        public bool Success { get; set; }
        public List<string> Errors { get; set; }
    }
}
