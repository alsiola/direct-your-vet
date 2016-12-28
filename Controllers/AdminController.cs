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
using DYV.Services.Practices;
using DYV.Models.PracticeViewModels;
using DYV.Services;
using DYV.Services.User;
using DYV.Models.User;
using Microsoft.AspNetCore.Identity;
using DYV.Services.Admin;

namespace DYV.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IPracticesService _practicesService;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public AdminController(IAdminService adminService, IPracticesService practicesService, IEmailSender emailSender, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _adminService = adminService;
            _practicesService = practicesService;
            _emailSender = emailSender;
            _userManager = userManager;
            _context = context;
        }

        // GET: Practices
        public async Task<IActionResult> Index()
        {
            return View(await _practicesService.GetPracticesListViewModel());
        }

        //// GET: Admin/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var practice = await _practicesService.GetPracticeDetailsViewModel(id.Value);

        //    if (practice == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(practice);
        //}

        // GET: Admin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] AddPracticeViewModel addPracticeViewModel)
        {
            if (ModelState.IsValid)
            {
                await _practicesService.AddNewPractice(addPracticeViewModel);
                return RedirectToAction("Index");
            }
            return View(addPracticeViewModel);
        }

        // GET: Admin/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null || !id.HasValue)
            {
                return NotFound();
            }

            ViewData["practiceId"] = id.Value;

            return View();
        }

        //GET: Admin/EditDetails/5
        public async Task<IActionResult> EditDetails(int id)
        {
            EditPracticeViewModel practice = await _practicesService.GetEditPracticeViewModel(id);

            if (practice == null)
            {
                return NotFound();
            }

            return Json(practice);
        }

        // POST: Admin/Edit/5
        [HttpPost]
        public async Task<IActionResult> SaveDetails([FromBody] EditPracticeViewModel editPracticeViewModel)
        {
            if (editPracticeViewModel == null)
                return Json(new JsonResponse(false, "Server did not receive data."));

            if (string.IsNullOrEmpty(editPracticeViewModel.Name))
                return Json(new JsonResponse(false, "Name is not set."));

            try
            {
                return Json(await _practicesService.UpdatePractice(editPracticeViewModel));
            }
            catch (DbUpdateConcurrencyException)
            {
                return Json(new JsonResponse(false, "Concurrency error."));
            }
        }

        //POST Admin/RemoveUserFromPractice
        [HttpPost]
        public async Task<IActionResult> RemoveUserFromPractice([FromBody] SubscriberPractice subscriberPractice)
        {
            if (subscriberPractice == null || string.IsNullOrEmpty(subscriberPractice.SubscriberUserId) || subscriberPractice.PracticeId == 0)
                return Json(new JsonResponse(false, "Server did not receive data."));

            try
            {
                return Json(await _practicesService.RemoveSubscriberFromPractice(subscriberPractice));
            }
            catch
            {
                return Json(new JsonResponse(false, "Concurrency error."));
            }
        }

        //POST: Admin/MakeUserManager
        [HttpPost]
        public async Task<IActionResult> MakeUserManager([FromBody] SubscriberPractice subscriberPractice)
        {
            using (var transact = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    if (subscriberPractice == null || string.IsNullOrEmpty(subscriberPractice.SubscriberUserId) || subscriberPractice.PracticeId == 0)
                        return Json(new JsonResponse(false, "Server did not receive data."));

                    var response = await _practicesService.UpdateUserManagerStatus(subscriberPractice);

                    if (response.Success)
                    {
                        IdentityResult roleChange;

                        if (subscriberPractice.IsManager)
                        {
                            roleChange = await _userManager.AddToRoleAsync(await _adminService.GetUserById(subscriberPractice.SubscriberUserId), "PracticeManager");
                        } else
                        {
                            roleChange = await _userManager.RemoveFromRoleAsync(await _adminService.GetUserById(subscriberPractice.SubscriberUserId), "PracticeManager");
                        }

                        if (roleChange.Succeeded)
                        {
                            transact.Commit();
                            return Json(new JsonResponse());
                        }

                        transact.Rollback();
                        return Json(new JsonResponse(false, "Could not add user role."));
                    }

                    transact.Rollback();
                    return Json(new JsonResponse(false, "COuld not update subscriber practice."));
                }
                catch
                {
                    transact.Rollback();
                    return Json(new JsonResponse(false, "Concurrency error."));
                }
            }            
        }
    }
}
