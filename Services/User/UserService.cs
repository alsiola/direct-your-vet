using AutoMapper.QueryableExtensions;
using DYV.Data;
using DYV.Models;
using DYV.Models.AccountViewModels;
using DYV.Models.PracticeViewModels;
using DYV.Models.User;
using DYV.Services.Providers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DYV.Services.User
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IInviteCodeProvider _inviteCodeProvider;
        private readonly IDateProvider _dateProvider;
        private readonly IUserProvider _userProvider;

        public UserService(ApplicationDbContext context, IInviteCodeProvider inviteCodeProvider, IDateProvider dateProvider, IUserProvider userProvider)
        {
            _context = context;
            _inviteCodeProvider = inviteCodeProvider;
            _dateProvider = dateProvider;
            _userProvider = userProvider;
        }

        public async Task<UserInvite> CreateInvite(InviteViewModel inviteVM)
        {
            var invite = new UserInvite()
            {
                Email = inviteVM.InviteEmail,
                PracticeId = (await _context.Practices.Where(prac => prac.SubscriberPractices.Any(sp => sp.SubscriberUserId == _userProvider.GetUserId())).SingleAsync()).Id,
                CreationDate = _dateProvider.GetCurrentDateTime(),
                Code = _inviteCodeProvider.GetNewInviteCode(),
                Used = false
            };

            _context.UserInvites.Add(invite);
            await _context.SaveChangesAsync();

            return invite;
        }

        public async Task<InviteCodeResult> GetCodeDetails(string code)
        {
            UserInvite dbInvite = await _context.UserInvites.Where(invite => invite.Code == code).SingleOrDefaultAsync();

            if (dbInvite == null)
                return new InviteCodeResult() { Success = false, Error = "Code not found." };

            if (dbInvite.Used)
                return new InviteCodeResult() { Success = false, Error = "Code has already been used." };

            if (dbInvite.CreationDate.AddDays(7) < _dateProvider.GetCurrentDateTime())
                return new InviteCodeResult() { Success = false, Error = "Code expired." };

            dbInvite.Used = true;
            await _context.SaveChangesAsync();

            return new InviteCodeResult() { Success = true, Error = "", PracticeId = dbInvite.PracticeId };

        }

        public async Task AddUserToPractice(string id, int practiceId)
        {
            var subPrac = new SubscriberPractice() { PracticeId = practiceId, SubscriberUserId = id };

            _context.SubscriberPractices.Add(subPrac);

            await _context.SaveChangesAsync();
        }

        public async Task<List<SubscriberUserViewModel>> GetUsersForPracticeManager()
        {
            var mpids = GetCurrentUserManagedPracticeIds();


            return
                await _context.SubscriberUsers
                .Where(
                    su =>
                    su
                        .SubscriberPractices
                        .Any(
                            sp =>
                            mpids.Contains(sp.PracticeId)
                            )
                ).
                Select(su => new { User = su, sp = _context.SubscriberPractices.Where(z => mpids.Contains(z.PracticeId) && z.SubscriberUserId == su.Id).Single()  })
                .Select( anon => new SubscriberUserViewModel() { Id = anon.User.Id, Email = anon.User.Email, Name = anon.User.Name, IsManager = anon.sp.IsManager})
                .ToListAsync();
        }

        public async Task<SubscriberUserViewModel> GetUserById(string id)
        {
            return await
                _context.SubscriberUsers
                .Where(
                    su =>
                    su.Id == id &&
                    su
                        .SubscriberPractices
                        .Any(
                            sp =>
                            sp.PracticeId == (_context.SubscriberPractices
                                                .Where(
                                                    sp0 =>
                                                    sp0.SubscriberUserId == _userProvider.GetUserId()
                                                )
                                                .Single().PracticeId)
                            )
                )
                .ProjectTo<SubscriberUserViewModel>()
                .SingleAsync();

        }

        public async Task RemoveUserFromPractice(string id)
        {    
            _context.RemoveRange(
                        _context.SubscriberPractices.Where(
                        sp =>
                        sp.SubscriberUserId == id &&
                        GetCurrentUserManagedPracticeIds().Contains(sp.PracticeId)
                  )
            );

            await _context.SaveChangesAsync();
        }

        public List<int> GetCurrentUserManagedPracticeIds()
        {
            return
                _context
                .SubscriberPractices
                .Where(
                    sp =>
                    sp.SubscriberUserId == _userProvider.GetUserId() &&
                    sp.IsManager == true)
                .Select(sp => sp.PracticeId)
                .ToList();
        }
    }
}
