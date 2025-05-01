using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoHub.Data.Models
{
	internal class Customer
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string FirstName { get; set; }

		[Required]
		[MaxLength(50)]
		public string LastName { get; set; }

		[MaxLength(100)]
		[EmailAddress] // Email validation attribute
		public string Email { get; set; }

		[Required]
		[MaxLength(20)]
		public string PhoneNumber { get; set; }

		// Navigation property for sales related to this customer
		public virtual ICollection<Sale> Sales { get; set; }
	}
}
