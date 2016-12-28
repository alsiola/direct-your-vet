using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DYV.Models
{
    public class JsonResponse
    {
        public JsonResponse()
        {
            Success = true;
            Errors = new List<string>();
        }

        public JsonResponse(bool success, string error)
        {
            Success = success;
            Errors = new List<string>();
            Errors.Add(error);
        }

        public bool Success { get; set; }
        public List<string> Errors { get; set; }
        public object ReturnData { get; set; }
    }
}
