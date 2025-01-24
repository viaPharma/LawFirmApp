using System.Collections.Generic;
using System.Linq;
using LawFirmApp.Models;
using LawFirmApp.Data;
using Microsoft.EntityFrameworkCore;

namespace LawFirmApp.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            // Query the database for a matching username and password
            var user = await _context.Users
                .Include(u => u.Role) // Include the Role navigation property
                .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

            return user; // Return the user if found, otherwise null
        }
    }
}
