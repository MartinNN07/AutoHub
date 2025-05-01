using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoHub.Business.Services
{
    internal interface IBrandService
    {
        Task<Brand> CreateBrandAsync(Brand brand);

        // READ
        Task<IEnumerable<Brand>> GetAllBrandsAsync();
        Task<Brand> GetBrandByIdAsync(int id);
        Task<IEnumerable<Brand>> GetBrandsByNameAsync(string searchTerm);

        // UPDATE
        Task<Brand> UpdateBrandAsync(Brand brand);

        // DELETE
        Task<bool> DeleteBrandAsync(int id);
    }
}
