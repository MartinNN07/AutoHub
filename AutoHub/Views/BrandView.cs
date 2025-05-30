﻿using AutoHub.Business.Services.Interfaces;
using AutoHub.Data.Models;
using AutoHub.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoHub.Views
{
    // Implements the brand management UI for console applications
    public class BrandView : IBrandView
    {
        private readonly IBrandService _brandService;

        // Constructor injecting the brand service dependency
        public BrandView(IBrandService brandService)
        {
            _brandService = brandService;
        }

        // Displays the main menu and handles user input
        public async Task DisplayMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("========== Brand Management ==========");
                Console.WriteLine("1. View All Brands");
                Console.WriteLine("2. Find Brand by ID");
                Console.WriteLine("3. Search Brands by Name");
                Console.WriteLine("4. Add New Brand");
                Console.WriteLine("5. Update Brand");
                Console.WriteLine("6. Delete Brand");
                Console.WriteLine("0. Back to Main Menu");
                Console.WriteLine("=====================================");
                Console.Write("Enter your choice: ");

                // Read user input and handle menu selection
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    { 
                        case 1:
                            await DisplayAllBrands(); // List all brands
                            break;
                        case 2:
                            await FindBrandById(); // Find a brand by ID
                            break;
                        case 3:
                            await SearchBrandsByName(); // Search brands by name
                            break;
                        case 4:
                            await AddNewBrand(); // Add a new brand
                            break;
                        case 5:
                            await UpdateBrand(); // Update an existing brand
                            break;
                        case 6:
                            await DeleteBrand(); // Delete a brand
                            break;
                        case 0:
                            exit = true; // Exit the menu
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }

                if (!exit)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        // Fetches and displays all brands from the service
        public async Task DisplayAllBrands()
        {
            Console.Clear();
            Console.WriteLine("========== All Brands ==========");

            var brands = await _brandService.GetAllBrandsAsync();
            if (!brands.Any())
            {
                Console.WriteLine("No brands found in the database.");
                return;
            }

            foreach (var brand in brands)
            {
                await DisplayBrandDetails(brand);
                Console.WriteLine("---------------------------");
            }
        }

        // Finds a brand by its ID and displays it
        public async Task FindBrandById()
        {
            Console.Clear();
            Console.WriteLine("========== Find Brand by ID ==========");
            Console.Write("Enter Brand ID: ");

            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var brand = await _brandService.GetBrandByIdAsync(id);
                if (brand != null)
                {
                    await DisplayBrandDetails(brand);
                }
                else
                {
                    Console.WriteLine($"Brand with ID {id} not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID format. Please enter a number.");
            }
        }

        // Searches for brands by a name or partial name
        public async Task SearchBrandsByName()
        {
            Console.Clear();
            Console.WriteLine("========== Search Brands by Name ==========");
            Console.Write("Enter brand name (or part of brand name): ");
            string searchTerm = Console.ReadLine() ?? string.Empty;

            var brands = await _brandService.GetBrandsByNameAsync(searchTerm);
            if (!brands.Any())
            {
                Console.WriteLine($"No brands found matching '{searchTerm}'.");
                return;
            }

            foreach (var brand in brands)
            {
                await DisplayBrandDetails(brand);
                Console.WriteLine("---------------------------");
            }
        }

        // Prompts the user to input a new brand and adds it via the service
        public async Task AddNewBrand()
        {
            Console.Clear();
            Console.WriteLine("========== Add New Brand ==========");

            // Prompt for brand details
            try
            {
                var brand = new Brand();

                Console.Write("Enter Brand Name: ");
                brand.Name = Console.ReadLine() ?? string.Empty;
                if (string.IsNullOrWhiteSpace(brand.Name))
                {
                    Console.WriteLine("Brand name is required.");
                    return;
                }

                Console.Write("Enter Country of Origin (optional): ");
                brand.CountryOfOrigin = Console.ReadLine();

                var createdBrand = await _brandService.CreateBrandAsync(brand);
                Console.WriteLine($"Brand added successfully with ID: {createdBrand.Id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding brand: {ex.Message}");
            }
        }

        // Updates an existing brand's information
        public async Task UpdateBrand()
        {
            Console.Clear();
            await DisplayAllBrands(); // Show current list before updating
            Console.WriteLine("========== Update Brand ==========");
            Console.Write("Enter Brand ID to update: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID format. Please enter a number.");
                return;
            }

            // Fetch the existing brand
            var existingBrand = await _brandService.GetBrandByIdAsync(id);
            if (existingBrand == null)
            {
                Console.WriteLine($"Brand with ID {id} not found.");
                return;
            }

            await DisplayBrandDetails(existingBrand);

            Console.WriteLine("\nEnter new details (press Enter to keep current values):");

            // Prompt for new details
            Console.Write($"Name ({existingBrand.Name}): ");
            string name = Console.ReadLine() ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(name))
            {
                existingBrand.Name = name;
            }

            // Optional: Update country of origin
            Console.Write($"Country of Origin ({existingBrand.CountryOfOrigin}): ");
            string countryOfOrigin = Console.ReadLine() ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(countryOfOrigin))
            {
                existingBrand.CountryOfOrigin = countryOfOrigin;
            }

            try
            {
                var updatedBrand = await _brandService.UpdateBrandAsync(existingBrand);
                Console.WriteLine($"Brand with ID {updatedBrand.Id} updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating brand: {ex.Message}");
            }
        }

        // Deletes a brand by ID after confirmation
        public async Task DeleteBrand()
        {
            Console.Clear();
            await DisplayAllBrands(); // Show current brands for reference
            Console.WriteLine("========== Delete Brand ==========");
            Console.Write("Enter Brand ID to delete: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID format. Please enter a number.");
                return;
            }

            var brand = await _brandService.GetBrandByIdAsync(id);
            if (brand == null)
            {
                Console.WriteLine($"Brand with ID {id} not found.");
                return;
            }

            await DisplayBrandDetails(brand);

            // Warn if brand has associated cars
            if (brand.Cars != null && brand.Cars.Any())
            {
                Console.WriteLine($"\nThis brand has {brand.Cars.Count} associated cars. Deleting it may affect these cars.");
            }

            Console.Write("\nAre you sure you want to delete this brand? (Y/N): ");
            string confirmation = Console.ReadLine() ?? string.Empty;

            // Confirm deletion
            if (confirmation.Trim().ToUpper().StartsWith("Y"))
            {
                bool result = await _brandService.DeleteBrandAsync(id);
                if (result)
                {
                    Console.WriteLine($"Brand with ID {id} deleted successfully.");
                }
                else
                {
                    Console.WriteLine($"Failed to delete brand with ID {id}.");
                }
            }
            else
            {
                Console.WriteLine("Delete operation cancelled.");
            }
        }

        // Displays detailed information for a single brand
        public async Task DisplayBrandDetails(Brand brand)
        {
            Console.WriteLine($"ID: {brand.Id}");
            Console.WriteLine($"Name: {brand.Name}");
            Console.WriteLine($"Country of Origin: {brand.CountryOfOrigin ?? "Not specified"}");
            
            if (brand.Cars != null && brand.Cars.Any())
			{
                Console.WriteLine($"Number of Cars: {brand.Cars.Count}");
			}
        }
    }
}