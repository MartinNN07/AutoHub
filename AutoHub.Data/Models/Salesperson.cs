using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoHub.Data.Models
{
	internal class Salesperson
	{
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(20)]
        public string EmployeeNumber { get; set; }

        [Required]
        public DateTime HireDate { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }
    }
}
