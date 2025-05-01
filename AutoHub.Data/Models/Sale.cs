using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoHub.Data.Models
{
	internal class Sale
	{
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime SaleDate { get; set; }

        [Required]
        [Range(0.01, 15000000)]
        public decimal SalePrice { get; set; }

        [Required]
        [Index(IsUnique = true)]
        public int CarId { get; set; }

        [Required]
        public virtual Car Car { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public virtual Customer Customer { get; set; }

        [Required]
        public int SalespersonId { get; set; }

        [Required]
        public virtual Salesperson Salesperson { get; set; }

    }
}
