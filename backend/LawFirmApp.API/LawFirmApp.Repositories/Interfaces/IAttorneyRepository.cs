using System.Collections.Generic;
using System.Threading.Tasks;
using LawFirmApp.Models;

namespace LawFirmApp.Repositories
{
    public interface IAttorneyRepository
    {
        Task<IEnumerable<Attorney>> GetAllAsync();
        Task<Attorney> GetByIdAsync(int id);
        Task<Attorney> AddAsync(Attorney attorney);
        Task<Attorney> UpdateAsync(Attorney attorney);
        Task<bool> DeleteAsync(int id);
    }
}
