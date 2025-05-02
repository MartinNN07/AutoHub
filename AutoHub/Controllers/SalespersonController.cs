using AutoHub.Business.Services.Interfaces;
using AutoHub.Controllers.Interfaces;
using AutoHub.Data.Models;
using AutoHub.Views;
using AutoHub.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoHub.Controllers
{
	public class SalespersonController : ISalespersonController
	{
		private readonly ISalespersonService _salespersonService;
		private readonly SalespersonView _salespersonView;

		public SalespersonController(ISalespersonService salespersonService)
		{
			_salespersonService = salespersonService;
			_salespersonView = new SalespersonView(salespersonService);
		}

		public async Task Run()
		{
			await _salespersonView.DisplayMenu();
		}

		public async Task<IEnumerable<Salesperson>> GetAllSalespersonsAsync()
		{
			return await _salespersonService.GetAllSalespersonAsync();
		}

		public async Task<Salesperson> GetSalespersonByIdAsync(int id)
		{
			return await _salespersonService.GetSalespersonByIdAsync(id);
		}

		public async Task<IEnumerable<Salesperson>> SearchSalespersonsByFirstNameAsync(string searchTerm)
		{
			return await _salespersonService.GetSalespersonByFirstNameAsync(searchTerm);
		}

		public async Task<Salesperson> CreateSalespersonAsync(Salesperson salesperson)
		{
			if (string.IsNullOrWhiteSpace(salesperson.FirstName))
			{
				throw new ArgumentException("First name is required.");
			}

			if (string.IsNullOrWhiteSpace(salesperson.LastName))
			{
				throw new ArgumentException("Last name is required.");
			}

			if (string.IsNullOrWhiteSpace(salesperson.EmployeeNumber))
			{
				throw new ArgumentException("Employee number is required.");
			}

			if (salesperson.HireDate == default)
			{
				throw new ArgumentException("Hire date is required.");
			}

			return await _salespersonService.CreateSalespersonAsync(salesperson);
		}

		public async Task<Salesperson> UpdateSalespersonAsync(Salesperson salesperson)
		{
			var existingSalesperson = await _salespersonService.GetSalespersonByIdAsync(salesperson.Id);
			if (existingSalesperson == null)
			{
				throw new KeyNotFoundException($"Salesperson with ID {salesperson.Id} not found.");
			}

			if (string.IsNullOrWhiteSpace(salesperson.FirstName))
			{
				throw new ArgumentException("First name is required.");
			}

			if (string.IsNullOrWhiteSpace(salesperson.LastName))
			{
				throw new ArgumentException("Last name is required.");
			}

			if (string.IsNullOrWhiteSpace(salesperson.EmployeeNumber))
			{
				throw new ArgumentException("Employee number is required.");
			}

			if (salesperson.HireDate == default)
			{
				throw new ArgumentException("Hire date is required.");
			}

			return await _salespersonService.UpdateSalespersonAsync(salesperson);
		}

		public async Task<bool> DeleteSalespersonAsync(int id)
		{
			var existingSalesperson = await _salespersonService.GetSalespersonByIdAsync(id);
			if (existingSalesperson == null)
			{
				return false;
			}

			return await _salespersonService.DeleteSalespersonAsync(id);
		}
	}
}