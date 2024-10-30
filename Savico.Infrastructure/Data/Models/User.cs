namespace Savico.Core.Models
{
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(3)] // Example: "USD", "EUR", etc.
        public string Currency { get; set; }

        public ICollection<Goal> FinancialGoals { get; set; } = new List<Goal>();
    }
}
