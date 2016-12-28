using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DYV.Data;
using DYV.Models.User;
using Microsoft.AspNetCore.Authorization;
using DYV.Models.PracticeViewModels;
using DYV.Services;
using DYV.Services.User;
using static DYV.Controllers.ManageController;

namespace DYV.Views.User
{  
    [Authorize(Roles = "PracticeManager")]
    public class ManagePracticeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IEmailSender _emailSender;

        public ManagePracticeController(IUserService userService, IEmailSender emailSender)
        {
            _userService = userService;
            _emailSender = emailSender; 
        }

        // GET: Users
        public async Task<IActionResult> Index(ManageMessageId? message = null)
        {
            ViewData["StatusMessage"] =
                message == ManageMessageId.InviteSent ? "Your invite has been sent."
                : "";

            return View(await _userService.GetUsersForPracticeManager());
        }

        //GET: Practices/SendInvite/3
        public IActionResult SendInvite()
        {
            return View(new InviteViewModel());
        }

        //POST: Practices/SendInvite/3
        [HttpPost]
        public async Task<IActionResult> SendInvite([Bind("Id, InviteEmail, Message")] InviteViewModel inviteVM)
        {
            if (ModelState.IsValid)
            {
                var invite = await _userService.CreateInvite(inviteVM);

                var replacements = new Dictionary<string, string>();
                replacements.Add("link", Url.Action("InviteSignUp", "Account", new { code = invite.Code }, protocol: HttpContext.Request.Scheme));

                if (!string.IsNullOrEmpty(inviteVM.Message))
                {
                    replacements.Add("message", inviteVM.Message);
                }

                await _emailSender.SendEmailAsync(invite.Email, "You have been invited to join Direct Your Vet", replacements, SparkPostTemplates.SubscriberInvite);
                
                return RedirectToAction("Index", new { Message = ManageMessageId.InviteSent });
            }

            return RedirectToAction(nameof(SendInvite));
        }

        //GET: ManagePractice/RemoveUser/userid
        public async Task<IActionResult> RemoveUser(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            return View( await _userService.GetUserById(id));
        }

        //POST: ManagePractice/RemoveUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveUser(SubscriberUserViewModel vm)
        {
            if (string.IsNullOrEmpty(vm.Id))
                return NotFound();

            await _userService.RemoveUserFromPractice(vm.Id);            

            return RedirectToAction("Index");
        }
    }
}
