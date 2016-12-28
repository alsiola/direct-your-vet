using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DYV.Models;
using DYV.Models.SubscriberDashboard;
using Microsoft.AspNetCore.Mvc;

namespace DYV.Services.DayListService
{
    public interface IDayListService
    {
        Task<JsonResponse> CreateDayList(CreateDayListViewModel model);
        Task<DayListDashboardViewModel> GetDayListsForCurrentUser();
        Task<DayListDashboardViewModel> GetDayListsForCurrentUser(int limit);
        Task<DayListViewModel> GetDayList(int value);
        Task<JsonResponse> UpdateZooms(SaveZoomLevelViewModel model);
    }
}
