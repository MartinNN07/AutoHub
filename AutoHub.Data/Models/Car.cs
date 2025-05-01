using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoHub.Data.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Model { get; set; }
        [Required]
        [Range(1900, 2026)]
        public int Year { get; set; }
        [Required]
        [Range(1000, 10000000)]
        public double Price { get; set; }
        [MaxLength(50)]
        public string? EngineType { get; set; }
        [Range(0, 2000000)]
        public int? Mileage { get; set; }
        [MaxLength(30)]
        public string? Color { get; set; }
        [Required]
        [DefaultValue(true)]
        public bool IsAvailable { get; set; }
        [Required]
        public int BrandId { get; set; }
        [Required]
        public Brand Brand { get; set; }
}
}
