using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DYV.Models.PracticeViewModels;
using DYV.Data;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using DYV.Models;
using DYV.Models.PlaceViewModels;
using DYV.Services.EFUpdate;
using DYV.Services.Providers;
using DYV.Models.AccountViewModels;
using DYV.Models.User;
using DYV.Models.Purchase;

namespace DYV.Services.Practices
{
    public class PracticesService : IPracticesService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IDateProvider _dateProvider;
        private readonly IUserProvider _userProvider;
        private readonly IInviteCodeProvider _inviteCodeProvider;

        public PracticesService(ApplicationDbContext context, IMapper mapper, IDateProvider dateProvider, IUserProvider userProvider, IInviteCodeProvider inviteCodeProvider)
        {
            _context = context;
            _mapper = mapper;
            _dateProvider = dateProvider;
            _userProvider = userProvider;
            _inviteCodeProvider = inviteCodeProvider;
        }

        public async Task<PracticeListViewModel> GetPracticesListViewModel()
        {
            return new PracticeListViewModel()
            {
                Practices = await _context.Practices.ProjectTo<PracticeListItemViewModel>().ToListAsync()
            };
        }

        public async Task AddNewPractice(AddPracticeViewModel addPracticeViewModel)
        {
           _context.Practices.Add(_mapper.Map<Practice>(addPracticeViewModel));
            await _context.SaveChangesAsync();
        }

        public async Task Unshare(int practiceId)
        {
            var clientPractices = await _context.ClientPractices.Where(cp => cp.ClientUserId == _userProvider.GetUserId() && cp.PracticeId == practiceId).ToListAsync();
            _context.ClientPractices.RemoveRange(clientPractices);

            await _context.SaveChangesAsync();
        }

        public async Task<PracticeDetailsViewModel> GetPracticeDetailsViewModel(int practiceId)
        {
            var practiceDetailsViewModel = await _context.Practices.Where(p => p.Id == practiceId).ProjectTo<PracticeDetailsViewModel>().SingleOrDefaultAsync();

            if (practiceDetailsViewModel == null)
                return null;

            return null;

        }

        public async Task<EditPracticeViewModel> GetEditPracticeViewModel(int practiceId)
        {
            return
                await _context.Practices.Where(p => p.Id == practiceId)
                    .Select(p =>
                        new EditPracticeViewModel()
                        {
                            Id = p.Id,
                            Name = p.Name,
                            SubscriberUsers = p.SubscriberPractices.Select(
                                                    q =>
                                                        _mapper.Map<SubscriberUser,SubscriberUserViewModel>(q.SubscriberUser, m => m.AfterMap((x,z) => z.setManager(q.IsManager)))
                                                )
                                                .ToList()
                        })
                     .SingleOrDefaultAsync();
        }

        public async Task<JsonResponse> UpdatePractice(EditPracticeViewModel editPracticeViewModel)
        {
            var dbPractice = await _context.Practices.Where(p => p.Id == editPracticeViewModel.Id).SingleOrDefaultAsync();

            if (dbPractice == null)
                return new JsonResponse(false, "Practice not found.");

            dbPractice.UpdateProperties(editPracticeViewModel);

            await _context.SaveChangesAsync();

            return new JsonResponse() { Success = true };
        }

        public async Task<string> GetCurrentUserPracticeName()
        {
            return (await _context.Practices.Where(prac => prac.SubscriberPractices.Any(sp => sp.SubscriberUserId == _userProvider.GetUserId())).SingleOrDefaultAsync())?.Name;
        }

        public async Task<string> GetPracticeNameById(int id)
        {
            return (await _context.Practices.Where(prac => prac.Id == id).SingleOrDefaultAsync())?.Name;
        }

        public async Task<Practice> GetPracticeBySignupSlug(string slug)
        {
            return await _context.Practices.Where(
                p => p.SignupSlug == slug
                )
                .SingleOrDefaultAsync();
        }

        public async Task<string> GetSignupSlugForCurrentManagerPractice()
        {
            return 
                (await _context
                    .Practices
                    .Where(
                        p =>
                        p.SubscriberPractices.Any(
                            sp => 
                                sp.SubscriberUserId == _userProvider.GetUserId() && 
                                sp.IsManager
                        )
                    )
                    .SingleOrDefaultAsync())?.SignupSlug;
        }

        public async Task<JsonResponse> RemoveSubscriberFromPractice(SubscriberPractice subscriberPractice)
        {
            try
            {
                _context.SubscriberPractices.Remove(
                    await _context
                        .SubscriberPractices
                        .Where(
                            sp =>
                            sp.PracticeId == subscriberPractice.PracticeId &&
                            sp.SubscriberUserId == subscriberPractice.SubscriberUserId)
                        .SingleAsync()
                 );

                await _context.SaveChangesAsync();

                return new JsonResponse();
            }
            catch
            {
                return new JsonResponse(false, "An error occurred removing the entity.");
            }
        }

        public async Task<JsonResponse> UpdateUserManagerStatus(SubscriberPractice subscriberPractice)
        {
            try
            {

                var subPrac = await _context
                    .SubscriberPractices
                    .Where(
                        sp =>
                        sp.PracticeId == subscriberPractice.PracticeId &&
                        sp.SubscriberUserId == subscriberPractice.SubscriberUserId)
                    .SingleAsync();

                subPrac.IsManager = subscriberPractice.IsManager;

                await _context.SaveChangesAsync();

                return new JsonResponse();
            }
            catch
            {
                return new JsonResponse(false, "An error occurred removing the entity.");
            }
        }

        public async Task AddMessagePurchaseToPractice(int purchaseType, int qty)
        { 
            var practice = await _context.Practices.Where(prac => prac.SubscriberPractices.Any(sp => sp.SubscriberUserId == _userProvider.GetUserId())).SingleOrDefaultAsync();

            PurchaseCost pc = await _context.PurchaseCosts.Where(p => p.Id == purchaseType).SingleOrDefaultAsync();

            if (pc != null)
            {
                if (practice.MessagePurchases == null)
                {
                    practice.MessagePurchases = new List<MessagePurchase>();
                }

                practice.MessagePurchases.Add(new MessagePurchase()
                    {
                        NumberPurchased = pc.Quantity * qty,
                        PurchaseType = pc.PurchaseType,
                        DoExpire = false,
                        ExpiryDate = DateTime.Now
                    });

                await _context.SaveChangesAsync();
            }
        }
    }
}
