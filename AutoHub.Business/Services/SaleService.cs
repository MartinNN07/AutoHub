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
    public class SaleService : ISaleService
    {
        private readonly AutoHubDbContext _context;

        public SaleService(AutoHubDbContext context)
        {
            _context = context;
        }

        public async Task<Sale> CreateSaleAsync(Sale sale)
        {
            if (sale == null)
                throw new ArgumentNullException(nameof(sale));

            await _context.Sales.AddAsync(sale);
            await _context.SaveChangesAsync();

            return sale;
        }

        public async Task<bool> DeleteSaleAsync(int id)
        {
            var sale = await _context.Sales.FindAsync(id);

            if (sale == null)
                return false;

            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Sale>> GetAllSalesAsync()
        {
            return await _context.Sales.ToListAsync();
        }

        public async Task<Sale> GetSaleByIdAsync(int id)
        {
            return await _context.Sales
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Sale>> GetSalesByCarModelAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetAllSalesAsync();

            return await _context.Sales
                .Where(s => s.Car.Model.ToLower().Contains(searchTerm.ToLower()))
                .ToListAsync();
        }

        public async Task<Sale> UpdateSaleAsync(Sale sale)
        {
            if (sale == null)
                throw new ArgumentNullException(nameof(sale));

            var existingSale = await _context.Sales.FindAsync(sale.Id);

            if (existingSale == null)
                throw new KeyNotFoundException($"Sale with ID {sale.Id} not found");

            _context.Entry(existingSale).CurrentValues.SetValues(sale);

            await _context.SaveChangesAsync();

            return existingSale;
        }
    }
}
