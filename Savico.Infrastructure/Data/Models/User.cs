namespace Savico.Core.Models
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Savico.Infrastructure.Data.Contracts;
    using System.ComponentModel.DataAnnotations;
    using static Savico.Infrastructure.Data.Constants.DataConstants.UserConstants;

    public class User : ISoftDeletable
    {
        [Key]
        [Comment("User identifier")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(UsernameMaxLength)]
        [Comment("User's username")]
        public string Username { get; set; } = null!;

        [Required]
        [MaxLength(FirstNameMaxLength)]
        [Comment("User's first name")]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(LastNameMaxLength)]
        [Comment("User's last name")]
        public string LastName { get; set; } = null!;

        [Required]
        [EmailAddress]
        [MaxLength(EmailMaxLength)]
        [Comment("User's email address")]
        public string Email { get; set; } = null!;

        [Required]
        [MaxLength(PasswordMaxLength)]
        [Comment("User's password")]
        public string Password { get; set; } = null!;

        [Required]
        [MaxLength(CurrencyMaxLength)] // Example: "USD", "EUR", etc.
        [Comment("User's currency")]
        public string Currency { get; set; } = null!;

        [Comment("User's profile picture")]
        public string? ProfilePicture { get; set; }

        [Comment("Indicates if the user is soft-deleted")]
        public bool IsDeleted { get; set; } = false;

        public ICollection<Goal> Goals { get; set; } = new HashSet<Goal>();

        public ICollection<Income> Incomes { get; set; } = new HashSet<Income>();

        public ICollection<Expense> Expenses { get; set; } = new HashSet<Expense>();

        public ICollection<Budget> Budgets { get; set; } = new HashSet<Budget>();
    }
}
