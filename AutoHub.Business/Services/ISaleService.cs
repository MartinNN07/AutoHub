using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoHub.Data.Models;

namespace AutoHub.Business.Services
{
    public interface ISaleService
    {
        Task<Sale> CreateSaleAsync(Sale sale);

        Task<IEnumerable<Sale>> GetAllSalesAsync();
        Task<Sale> GetSaleByIdAsync(int id);
        Task<IEnumerable<Sale>> GetSalesByCarModelAsync(string searchTerm);

        Task<Sale> UpdateSaleAsync(Sale sale);

        Task<bool> DeleteSaleAsync(int id);
    }
}
