using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DYV.Data;
using DYV.Models;
using Microsoft.AspNetCore.Authorization;
using DYV.Services.Places;
using DYV.Models.Ajax;
using DYV.Models.SubscriberDashboard;
using DYV.Services.Practices;
using System.Threading;
using DYV.Services.DayListService;
using DYV.Models.PlaceViewModels;

namespace DYV.Views
{
    [Authorize(Roles = "Subscriber, Admin")]
    public class SubscriberDashboardController : Controller
    {
        private readonly IPlacesService _placesService;
        private readonly IPracticesService _practiceService;
        private readonly IDayListService _dayListService;

        public SubscriberDashboardController(IPlacesService placesService, IPracticesService practiceService, IDayListService dayListService)
        {
            _placesService = placesService;
            _practiceService = practiceService;
            _dayListService = dayListService;
        }

        // GET: SubscriberDashboard
        public async Task<IActionResult> Index()
        {
            return View(new SubscriberDashboardIndexViewModel()
            {
                PracticeName = await _practiceService.GetCurrentUserPracticeName()
            });
        }

        // GET: SubscriberDashboard/3
        [Route("SubscriberDashboard/Admin/{practiceId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index(int practiceId)
        {
            return View(new SubscriberDashboardIndexViewModel()
            {
                PracticeName = await _practiceService.GetPracticeNameById(practiceId)
            });
        }

        // GET: SubscriberDashboard/SearchPlaces/
        [Route("SubscriberDashboard/SearchPlaces")]
        public async Task<IActionResult> SearchPlaces(string placeTerm, string clientTerm, string addressTerm, int page, int take)
        {
            if (placeTerm == null)
                placeTerm = "";

            if (clientTerm == null)
                clientTerm = "";

            if (addressTerm == null)
                addressTerm = "";

            return Json(
                await _placesService.GetPlacesListViewModelForCurrentSubscriber(
                    new SubscriberDashboardAjaxSearchViewModel()
                    {
                        AddressTerm = addressTerm.ToLower(),
                        ClientNameTerm = clientTerm.ToLower(),
                        PlaceNameTerm = placeTerm.ToLower(),
                        Page = page - 1,
                        Take = take
                    }));     
        }

        // GET: SubscriberDashboard/Admin/3/SearchPlaces/
        [Route("SubscriberDashboard/Admin/{practiceId}/SearchPlaces")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> SearchPlaces(int practiceId, string placeTerm, string clientTerm, string addressTerm, int page, int take)
        {
            if (placeTerm == null)
                placeTerm = "";

            if (clientTerm == null)
                clientTerm = "";

            if (addressTerm == null)
                addressTerm = "";

            return Json(
                await _placesService.GetPlacesListViewModelForPracticeIfAdmin(
                    practiceId,
                    new SubscriberDashboardAjaxSearchViewModel()
                    {
                        AddressTerm = addressTerm.ToLower(),
                        ClientNameTerm = clientTerm.ToLower(),
                        PlaceNameTerm = placeTerm.ToLower(),
                        Page = page - 1,
                        Take = take
                    }));
        }

        // GET: SubscriberDashboard/SaveDayList
        [Route("SubscriberDashboard/SaveDayList")]
        [Authorize(Roles = "Subscriber")]
        [HttpPost]
        public async Task<IActionResult> SaveDayList([FromBody] CreateDayListViewModel model)
        {
            return Json(await _dayListService.CreateDayList(model));
        }

        //GET: SubscriberDashboard/DayLists
        [Route("SubscriberDashboard/DayLists/")]
        [Authorize(Roles = "Subscriber")]
        public async Task<IActionResult> DayLists()
        {
            return Json(
                await _dayListService.GetDayListsForCurrentUser(5)
            );
        }

        //GET: SubscriberDashboard/DayLists
        [Route("SubscriberDashboard/DayLists/All")]
        [Authorize(Roles = "Subscriber")]
        public async Task<IActionResult> DayListsAll()
        {
            return View(
                await _dayListService.GetDayListsForCurrentUser()
            );
        }

        //GET: SubscriberDashboard/GetDayListDetails/17
        [Route("SubscriberDashboard/GetDayListDetails/{id}")]
        [Authorize(Roles = "Subscriber")]
        public async Task<IActionResult> GetDayListDetails(int id)
        {
            return Json(await _dayListService.GetDayList(id));
        }

        //GET: SubscriberDashboard/DayListDetails/17
        [Route("SubscriberDashboard/DayLists/{id}")]
        [Authorize(Roles = "Subscriber, PracticeManager")]
        public IActionResult DayListDetails(int id)
        {
            ViewData["dayListId"] = id;
            return View();
        }

        //GET: SubscriberDashboard/SaveDayListDetails
        [Route("SubscriberDashboard/SaveDayListDetails")]
        [Authorize(Roles = "Subscriber")]
        public async Task<IActionResult> SaveDayListDetails([FromBody] SaveZoomLevelViewModel model)
        {
            return Json(await _dayListService.UpdateZooms(model));
        }

        //GET: SubscriberDashboard/DeleteDayList/85
        [Route("SubscriberDashboard/DeleteDayList/{id}")]
        [Authorize(Roles = "Subscriber")]
        public async Task<IActionResult> DeleteDayList(int id)
        {
            return View(await _dayListService.GetDayList(id));
        }

        //GET: /SubscriberDashboard/Details/5
        [Authorize(Roles = ("Client, Subscriber, Admin"))]
        [Route("SubscriberDashboard/Details/{id?}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (!id.HasValue || id == null)
                return NotFound();

            PlaceDetailsViewModel place;

            if (User.IsInRole("Admin"))
                place = await _placesService.GetPlaceByIdForAdmin(id.Value);
            else
                place = await _placesService.GetPlaceById(id.Value);

            if (place == null)
                return NotFound();

            return View(place);
        }
    }
}
