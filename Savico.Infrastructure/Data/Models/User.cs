﻿namespace Savico.Core.Models
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Savico.Infrastructure.Data.Models;
    using System.ComponentModel.DataAnnotations;
    using static Savico.Infrastructure.Data.Constants.DataConstants;
    using static Savico.Infrastructure.Data.Constants.DataConstants.UserConstants;

    public class User : IdentityUser
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [StringLength(FirstNameMaxLength, MinimumLength = FirstNameMinLength, ErrorMessage = LengthErrorMessage)]
        [Comment("User's first name")]
        public string? FirstName { get; set; }

        [Required]
        [StringLength(LastNameMaxLength, MinimumLength = LastNameMinLength, ErrorMessage = LengthErrorMessage)]
        [Comment("User's last name")]
        public string? LastName { get; set; }

        [Required]
        [StringLength(CurrencyMaxLength, MinimumLength = CurrencyMinLength, ErrorMessage = LengthErrorMessage)]
        // example: "USD", "EUR", etc.
        [Comment("User's currency")]
        public string? Currency { get; set; }

        public int BudgetId {  get; set; }

        public Budget Budget { get; set; }

        [Comment("Indicates if the user is soft-deleted")]
        public bool IsDeleted { get; set; } = false;

        public ICollection<Goal> Goals { get; set; } = new HashSet<Goal>();

        public ICollection<Income> Incomes { get; set; } = new HashSet<Income>();

        public ICollection<Expense> Expenses { get; set; } = new HashSet<Expense>();

        public ICollection<Report> Reports { get; set; } = new HashSet<Report>();
    }
}
