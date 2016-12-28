using DYV.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DYV.Models.Purchase.ViewModels;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DYV.Models.Purchase;
using Stripe;
using DYV.Services.Practices;
using DYV.Models.ClientRelations;
using AutoMapper;
using DYV.Models;
using DYV.Services.User;
using DYV.Services.Providers;

namespace DYV.Services.ClientRelations
{
    public class ClientRelationsService : IClientRelationsService
    {
        private readonly ApplicationDbContext _context;
        private readonly IPracticesService _practicesService;
        private readonly IUserProvider _userProvider;
        private readonly IDateProvider _dateProvider;
        private readonly ISmsSender _smsSender;
        private readonly IUserService _userService;
        private readonly IInviteCodeProvider _inviteCodeProvider;

        public ClientRelationsService(ApplicationDbContext context, IPracticesService practicesService, IUserProvider userProvider, IDateProvider dateProvider, ISmsSender smsSender, IUserService userService, IInviteCodeProvider inviteCodeProvider)
        {
            _context = context;
            _practicesService = practicesService;
            _userProvider = userProvider;
            _dateProvider = dateProvider;
            _smsSender = smsSender;
            _userService = userService;
            _inviteCodeProvider = inviteCodeProvider;
        }

        public async Task<ClientRelationsIndexViewModel> GetIndexViewModel()
        {
            return await _context
                .Practices
                .FilterByCurrentUserManagedPractices(_userProvider.GetUserId())
                .Include(p => p.MessagePurchases)
                .ProjectTo<ClientRelationsIndexViewModel>()
                .FirstOrDefaultAsync();
        }

        public async Task<PurchaseCostsViewModel> GetPurchaseCostsViewModel()
        {
            return new PurchaseCostsViewModel()
            {
                PurchaseOptions = await _context.PurchaseCosts.ProjectTo<PurchaseCostItemViewModel>().ToListAsync(),
                SelectedPurchaseOption = ""
            };
        }

        public async Task<PaymentResult> MakePayment(StripeTokenViewModel model)
        {
            PurchaseCost pc = await _context.PurchaseCosts.Where(p => p.Id == model.purchaseType).SingleOrDefaultAsync();

            if (pc == null)
            {
                return new PaymentResult(false, "Purchase type not found.");
            }

            try
            {
                await new StripeChargeService().CreateAsync(new StripeChargeCreateOptions()
                {
                    Amount = (int)Math.Ceiling((pc.Price * 100)),
                    Currency = "gbp",
                    Description = await _practicesService.GetCurrentUserPracticeName() + " bought " + pc.Quantity * model.purchaseQty + " x " + pc.PurchaseType.ToString(),
                    SourceTokenOrExistingSourceId = model.stripeToken
                });

            }
            catch (StripeException StEx)
            {
                return new PaymentResult(false, StEx.Message);
            }

            return new PaymentResult(true);

        }

        public async Task<int> GetMsgQuotaForCurrentPractice(PurchaseType type)
        {
            var prac = await _context
                .Practices
                .FilterByCurrentUserManagedPractices(_userProvider.GetUserId())
                .Include(p => p.MessagePurchases)
                .SingleOrDefaultAsync();

            if (prac == null)
                throw new ArgumentException("Practice not found.");
            
            return prac.GetMsgQuota(type,_dateProvider.GetCurrentDateTime());
        }

        public async Task<SMSGroupSendResult> SendSMS(SendSMSViewModel vm)
        {
            var practice = await _context.Practices.FilterByCurrentUserManagedPractices(_userProvider.GetUserId()).Include(p => p.MessagePurchases).Include(p => p.MessageGroupSendResults).SingleOrDefaultAsync();
            int smsPerNumber = vm.Message.Length < 160 ? 1 : (int) Math.Floor((double)vm.Message.Length / 153);
            var smsNumRequired = vm.Numbers.Count(n => !vm.Numbers.Contains(n)) * smsPerNumber;

            SMSGroupSendResult result;
            List<SMSSendResult> results = new List<SMSSendResult>();

            if (practice == null)
            {
                result = new SMSGroupSendResult()
                {
                    SubscriberUserId = _userProvider.GetUserId(),
                    Error = "This user is not a practice manager, or the practice could not be found.",
                    TotalSucceeded = 0
                };
            } else if (practice.GetMsgQuota(PurchaseType.SMS, _dateProvider.GetCurrentDateTime()) < smsNumRequired) {
                result = new SMSGroupSendResult()
                {
                    PracticeId = practice.Id,
                    Error = "Insufficient SMS credit available.",
                    TotalSucceeded = 0
                };
            }
            else
            {
                List<string> existingNumbers = await _context.ClientUsers.Where(c => c.PhoneNumberConfirmed).Select(c => c.PhoneNumber).ToListAsync();
                
                await Task.WhenAll(
                    vm.Numbers
                        .Where(
                            n =>
                            !existingNumbers.Contains(n))
                        .Select(
                            async n =>
                            {
                                var userCode = _inviteCodeProvider.GetNewInviteCode(10);
                                results.Add(
                                    (await _smsSender.SendSmsAsync(n, vm.Message, userCode, vm.From))
                                );
                            }
                            
                        )
                    );

                result = new SMSGroupSendResult()
                {
                    SubscriberUserId = _userProvider.GetUserId(),
                    PracticeId = practice.Id,
                    TotalRequested = vm.Numbers.Count(),
                    TotalExisting = existingNumbers.Count(n => vm.Numbers.Contains(n)),
                    TotalSucceeded = results.Where(r => r.Success).Count(),
                    TotalFailed = results.Where(r => !r.Success).Count()
                };
            }            

            practice.MessageGroupSendResults.Add(result);

            foreach(SMSSendResult smr in results)
            {
                result.SendResults.Add(smr);
            }

            int numberToUse = result.TotalSucceeded * smsPerNumber;

            var messagePurchases =
                practice
                .MessagePurchases.Where(mp => mp.PurchaseType == PurchaseType.SMS && (!mp.DoExpire || mp.ExpiryDate.CompareTo(_dateProvider.GetCurrentDateTime()) > 0) && mp.MessagesRemaining() > 0)
                .OrderBy(mp => mp.ExpiryDate).ToList();
            
            int i = 0;

            while (numberToUse > 0 && i < messagePurchases.Count)
            {
                var currentMP = messagePurchases.ElementAt(i);
                if (currentMP.MessagesRemaining() >= numberToUse)
                {
                    currentMP.NumberUsed = currentMP.NumberUsed + numberToUse;
                    numberToUse = 0;
                } else
                {
                    numberToUse = numberToUse - currentMP.MessagesRemaining();
                    currentMP.NumberUsed = currentMP.NumberPurchased;
                }
                i++;
            }

            await _context.SaveChangesAsync();

            return result;
        }

        public async Task<List<GroupSendResultViewModel>> GetMessageGroupResults<T1>() where T1 : MessageGroupResult
        {            
            return await _context.Set<T1>()
                .Where(smsg => _userService.GetCurrentUserManagedPracticeIds().Contains(smsg.PracticeId))
                .Include(smsg => smsg.SubscriberUser)
                .ProjectTo<GroupSendResultViewModel>()
                .ToListAsync();
        }

        public async Task<List<T3>> GetMessageResultsByGroupId<T1,T2, T3>(int groupid) where T1 : class, IMessageSendResult<T2> where T2: MessageGroupResult where T3 : MessageSendResultViewModel
        {
            return await _context
               .Set<T1>()
               .Where(smsr => _userService.GetCurrentUserManagedPracticeIds().Contains(smsr.GroupSendResult.PracticeId))
               .Where(smsr => smsr.GroupSendResultId == groupid)
               .ProjectTo<T3>()
               .ToListAsync();
        }

        public async Task SetMarketingCodeAsOpened(string marketingSlug)
        {
            var s = await GetSMSSendResultByMarketingCode(marketingSlug);

            if (s == null)
                return;

            s.SlugOpened = true;

            await _context.SaveChangesAsync();
        }

        public async Task SetMarketingCodeAsRegistered(string marketingSlug)
        {
            var s = await GetSMSSendResultByMarketingCode(marketingSlug);

            if (s == null)
                return;

            s.RecipientSignedUp = true;

            await _context.SaveChangesAsync();
        }

        private async Task<SMSSendResult> GetSMSSendResultByMarketingCode(string marketingCode)
        {
            return await _context
                .SMSSendResults
                .Where(smsr => smsr.UniqueSignupSlug == marketingCode)
                .SingleOrDefaultAsync();
        }
    }
}
