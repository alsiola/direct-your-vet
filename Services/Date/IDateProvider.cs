using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DYV.Services
{
    public interface IDateProvider
    {
        DateTime GetCurrentDateTime();
    }
}
