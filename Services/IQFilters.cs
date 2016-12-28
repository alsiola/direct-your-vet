using DYV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DYV.Services
{
    public static class IQFilters
    {
        public static IQueryable<Practice> FilterByCurrentUserManagedPractices(this IQueryable<Practice> practices, string userId)
        {
            return practices.Where(p => p.SubscriberPractices.Any(sp => sp.SubscriberUserId == userId && sp.IsManager));
        }
    }
}
