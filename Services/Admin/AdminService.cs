using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DYV.Models.User;
using DYV.Data;
using Microsoft.EntityFrameworkCore;

namespace DYV.Services.Admin
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext _context;

        public AdminService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<SubscriberUser> GetUserById(string id)
        {
            return await _context.SubscriberUsers.Where(su => su.Id == id).SingleOrDefaultAsync();
        }
    }
}
