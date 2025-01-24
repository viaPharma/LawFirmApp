using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LawFirmApp.Repositories;
using Microsoft.EntityFrameworkCore;
using LawFirmApp.Data;
using LawFirmApp.Models;

namespace LawFirmApp.Repositories
{
    public class AttorneyRepository : IAttorneyRepository
    {
        private readonly AppDbContext _context;

        public AttorneyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Attorney>> GetAllAsync()
        {
            return await _context.Attorneys.ToListAsync();
        }

        public async Task<Attorney> GetByIdAsync(int id)
        {
            var result = await _context.Attorneys.FindAsync(id);

            if(result == null)
    {
                return null;
            }

            return result;
        }

        public async Task<Attorney> AddAsync(Attorney attorney)
        {
            await _context.Attorneys.AddAsync(attorney);
            await _context.SaveChangesAsync();
            return attorney;
        }

        public async Task<Attorney> UpdateAsync(Attorney attorney)
        {
            var existing = await _context.Attorneys.FindAsync(attorney.Id);

            if (existing == null)
                return null;

            existing.Name = attorney.Name;
            existing.Email = attorney.Email;
            existing.PhoneNumber = attorney.PhoneNumber;

            _context.Attorneys.Update(existing);
            await _context.SaveChangesAsync();

            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var attorney = await _context.Attorneys.FindAsync(id);
            if (attorney == null)
                return false;

            _context.Attorneys.Remove(attorney);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
