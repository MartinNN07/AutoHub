using AutoHub.Business.Services.Interfaces;
using AutoHub.Data.Database;
using AutoHub.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoHub.Business.Services
{
    public class SalespersonService : ISalespersonService
    {
        private readonly AutoHubDbContext _context;

        public SalespersonService(AutoHubDbContext context)
        {
            _context = context;
        }

        public async Task<Salesperson> CreateSalespersonAsync(Salesperson salesperson)
        {
            if (salesperson == null)
                throw new ArgumentNullException(nameof(salesperson));

            await _context.Salespersons.AddAsync(salesperson);
            await _context.SaveChangesAsync();

            return salesperson;
        }

        public async Task<bool> DeleteSalespersonAsync(int id)
        {
            var salesperson = await _context.Salespersons.FindAsync(id);

            if (salesperson == null)
                return false;

            _context.Salespersons.Remove(salesperson);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Salesperson>> GetAllSalespersonAsync()
        {
            return await _context.Salespersons.ToListAsync();
        }

        public async Task<IEnumerable<Salesperson>> GetSalespersonByFirstNameAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetAllSalespersonAsync();

            return await _context.Salespersons
                .Where(sp => sp.FirstName.ToLower().Contains(searchTerm.ToLower()))
                .ToListAsync();
        }

        public async Task<Salesperson> GetSalespersonByIdAsync(int id)
        {
            return await _context.Salespersons
                .FirstOrDefaultAsync(sp => sp.Id == id);
        }

        public async Task<Salesperson> UpdateSalespersonAsync(Salesperson salesperson)
        {
            if (salesperson == null)
                throw new ArgumentNullException(nameof(salesperson));

            var existingSalesperson = await _context.Salespersons.FindAsync(salesperson.Id);

            if (existingSalesperson == null)
                throw new KeyNotFoundException($"Sale with ID {salesperson.Id} not found");

            _context.Entry(existingSalesperson).CurrentValues.SetValues(salesperson);

            await _context.SaveChangesAsync();

            return existingSalesperson;
        }
    }
}
