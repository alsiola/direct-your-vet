using DYV.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DYV.Models.SubscriberDashboard;
using AutoMapper;
using DYV.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;

namespace DYV.Services.DayListService
{
    public class DayListService : IDayListService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserProvider _userProvider;
        private readonly IDateProvider _dateProvider;

        public DayListService(ApplicationDbContext context, IUserProvider userProvider, IDateProvider dateProvider)
        {
            _context = context;
            _userProvider = userProvider;
            _dateProvider = dateProvider;
        }

        public async Task<JsonResponse> CreateDayList(CreateDayListViewModel model)
        {
            var transact = _context.Database.BeginTransaction();
            var response = new JsonResponse();

            try
            {
                var dayList = new DayList()
                {
                    Name = model.name,
                    DateAdded = _dateProvider.GetCurrentDateTime(),
                    SubscriberUserId = _userProvider.GetUserId()
                };

                _context.DayLists.Add(dayList);
                await _context.SaveChangesAsync();

                foreach (var place in model.dayListPlaces)
                {
                    _context.PlaceDayLists.Add(new PlaceDayList()
                    {
                        PlaceId = place.Id,
                        DayListId = dayList.Id,
                        ZoomLevel = 8
                    });
                }

                await _context.SaveChangesAsync();

                transact.Commit();

                return response;
            }
            catch
            {
                transact.Rollback();
                response.Success = false;
                response.Errors.Add("An error occurred while writing to the database.");

                return response;
            }            
        }

        public async Task<DayListDashboardViewModel> GetDayListsForCurrentUser()
        {
            return await GetDayListsForCurrentUser(0);
        }

        public async Task<DayListDashboardViewModel> GetDayListsForCurrentUser(int limit)
        {
            IQueryable<DayList> dls = _context.DayLists
                                           .Where(dl => dl.SubscriberUserId == _userProvider.GetUserId())
                                           .OrderByDescending(p => p.DateAdded);            

            if (limit > 0)
                dls = dls.Take(limit);            

            return new DayListDashboardViewModel()
            {
                DayLists = await dls
                                .ProjectTo<DayListViewModel>()
                                .ToListAsync()
            };
        }

        public async Task<DayListViewModel> GetDayList(int id)
        {
            var dls = _context.DayLists
                .Where(
                    dl => dl.Id == id &&
                    dl.SubscriberUserId == _userProvider.GetUserId())
                .Include(q => q.PlaceDayLists)
                .ThenInclude(r => r.Place.ClientUser);


            return await dls.ProjectTo<DayListViewModel>()
                .SingleOrDefaultAsync();
        }

        public async Task<JsonResponse> UpdateZooms(SaveZoomLevelViewModel model)
        {
            JsonResponse response = new JsonResponse();
            string currentUserId = _userProvider.GetUserId();

            foreach (var pdl in model.placeZooms)
            {
                var dbPdl = await _context.PlaceDayLists.Where(p => p.DayListId == pdl.DayListId && p.PlaceId == pdl.PlaceId && p.DayList.SubscriberUserId == currentUserId).SingleOrDefaultAsync();

                if (dbPdl == null)
                {
                    response.Success = false;
                    response.Errors.Add($"Day list item not found with place id of {pdl.PlaceId}.");
                }

                dbPdl.ZoomLevel = pdl.ZoomLevel;
            }

            if (model.allPlacesMapZoom > 0)
            {
                var dbdl = await _context.DayLists.Where(dl => dl.Id == model.placeZooms.First().DayListId && dl.SubscriberUserId == currentUserId).SingleOrDefaultAsync();

                if (dbdl == null)
                {
                    response.Success = false;
                    response.Errors.Add("Daylist could not be found.");
                }

                dbdl.allPlacesMapZoom = model.allPlacesMapZoom;
            }            

            await _context.SaveChangesAsync();            

            return response;
        }
    }
}
