using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DYV.Services.Practices;
using Microsoft.AspNetCore.Authorization;
using DYV.Services.ClientRelations;
using DYV.Models.Purchase.ViewModels;
using DYV.Models.Purchase;
using DYV.Models;
using DYV.Models.ClientRelations;

namespace DYV.Controllers
{
    [Authorize(Roles = "PracticeManager")]
    public class ClientRelationsController : Controller
    {
        private readonly IPracticesService _practicesService;
        private readonly IClientRelationsService _clientRelationsService;

        public ClientRelationsController(IPracticesService practicesService, IClientRelationsService clientRelationsService)
        {
            _practicesService = practicesService;
            _clientRelationsService = clientRelationsService;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["signupSlug"] = Url.Action("Register", "Account", new { practiceSlug = await _practicesService.GetSignupSlugForCurrentManagerPractice() }, Request.Scheme);
            return View(await _clientRelationsService.GetIndexViewModel());
        }

        public async Task<IActionResult> Purchase()
        {
            return View(await _clientRelationsService.GetPurchaseCostsViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Purchase(StripeTokenViewModel model)
        {
            PaymentResult result = await _clientRelationsService.MakePayment(model);

            if (result.Success)
            {
                await _practicesService.AddMessagePurchaseToPractice(model.purchaseType, model.purchaseQty);
                return RedirectToAction("PurchaseSuccess");
            } else
            {
                return RedirectToAction("PurchaseFailure");
            }
        }

        public IActionResult PurchaseSuccess()
        {
            return View();
        }

        public IActionResult SendSMS()
        {
            return View();
        }
        
        public async Task<IActionResult> SendSMSData()
        {
            try
            {
                return Json(new JsonResponse()
                    {
                    ReturnData = new
                        {
                            smsRemaining = await _clientRelationsService.GetMsgQuotaForCurrentPractice(PurchaseType.SMS),
                            practiceSlug = await _practicesService.GetSignupSlugForCurrentManagerPractice()
                        }
                    }
                );
            }
            catch (ArgumentException ex)
            {
                return Json(new JsonResponse(false, ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> SendMessages([FromBody] SendSMSViewModel vm)
        {
            var smsGroupSendResult = await _clientRelationsService.SendSMS(vm);
            return Json(new JsonResponse(smsGroupSendResult.Error == null, smsGroupSendResult.Error)
            {
                ReturnData = Url.Action(nameof(ClientRelationsController.MessageGroupDetails), "ClientRelations", new { id = smsGroupSendResult.Id}, protocol: HttpContext.Request.Scheme)
            });
        }

        [Route("ClientRelations/MessageResults/{resultType}")]
        public async Task<IActionResult> ViewSendResults(PurchaseType resultType)
        {
            List<GroupSendResultViewModel> results = null;
            
            if (resultType == PurchaseType.SMS)
            {
                ViewData["type"] = PurchaseType.SMS.ToString();
                results = await _clientRelationsService.GetMessageGroupResults<SMSGroupSendResult>();
            } else if (resultType == PurchaseType.Email)
            {
                //results = await _clientRelationsService.GetAllSendResults<EmailGroupSendResult, EmailSendResult>();
            }

            if (results == null)
                return NotFound();

            return View(results);
        }

        [Route("ClientRelations/MessageResults/{resultType}/{id}")]
        public async Task<IActionResult> MessageGroupDetails(int id, PurchaseType resultType)
        {            
            if (resultType == PurchaseType.SMS)
            {
                var sendResults = await _clientRelationsService.GetMessageResultsByGroupId<SMSSendResult, SMSGroupSendResult, SMSSendResultViewModel>(id);
                if (sendResults.Count > 0)
                    return View(sendResults);
            }
            else if (resultType == PurchaseType.Email)
            {
                var sendResults = await _clientRelationsService.GetMessageResultsByGroupId<EmailSendResult, EmailGroupSendResult, EmailSendResultViewModel>(id);
                if (sendResults.Count > 0)
                    return View(sendResults);
            }

            return NotFound();
        }
    }
}