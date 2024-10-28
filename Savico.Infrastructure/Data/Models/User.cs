using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savico.Core.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(3)] // Example: "USD", "EUR", etc.
        public string Currency { get; set; }

        public ICollection<Goal> FinancialGoals { get; set; } = new List<Goal>();
    }
}
