using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoHub.Data.Models;

namespace AutoHub.Business.Services
{
    public interface ISalespersonService
    {
        Task<Salesperson> CreateSalespersonAsync(Salesperson salesperson);

        Task<IEnumerable<Salesperson>> GetAllSalespersonAsync();
        Task<Salesperson> GetSalespersonByIdAsync(int id);
        Task<IEnumerable<Salesperson>> GetSalespersonByFirstNameAsync(string searchTerm);

        Task<Salesperson> UpdateSalespersonAsync(Salesperson sale);

        Task<bool> DeleteSalespersonAsync(int id);
    }
}
