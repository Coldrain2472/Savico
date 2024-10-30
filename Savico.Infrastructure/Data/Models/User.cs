﻿namespace Savico.Core.Models
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using static Savico.Infrastructure.Data.Constants.DataConstants.UserConstants;

    public class User
    {
        [Key]
        [Comment("User identifier")]
        public Guid Id { get; set; }

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
        [StringLength(CurrencyMaxLength)] // Example: "USD", "EUR", etc.
        [Comment("User's currency")]
        public string Currency { get; set; } = null!;

        public ICollection<Goal> FinancialGoals { get; set; } = new List<Goal>();
    }
}
