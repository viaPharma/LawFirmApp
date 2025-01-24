using System.Collections.Generic;
using LawFirmApp.Models;

namespace LawFirmApp.Services
{
    public interface IUserService
    {
        Task<User> AuthenticateAsync(string username, string password);
    }
}