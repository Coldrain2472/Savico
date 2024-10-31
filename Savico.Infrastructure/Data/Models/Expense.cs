﻿namespace Savico.Core.Models
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static Savico.Infrastructure.Data.Constants.DataConstants;
    using static Savico.Infrastructure.Data.Constants.DataConstants.ExpenseConstants;

    public class Expense
    {
        [Comment("Expense identifier")]
        public int Id { get; set; }

        [Required]
        public string? UserId { get; set; }

        public User? User { get; set; }

        [Required]
        public int BudgetId { get; set; }

        public Budget? Budget { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = RangeErrorMessage)]
        [Comment("Expense's amount")]
        public decimal Amount { get; set; }

        [Required]
        [MaxLength(CategoryMaxLength)]
        [Comment("Expense's cactegory")]
        // "Food", "Transport", "Entertainment", "Utilities"
        public string? Category { get; set; }

        [Required]
        [Comment("Date of expense")]
        public DateTime Date { get; set; }

        [MaxLength(DescriptionMaxLength)]
        [Comment("Expense's description")]
        // Providing more details;  "Dinner at Italian Restaurant", "Monthly Netflix Subscription" etc..
        public string? Description { get; set; }

        [Comment("Indicates if the expense is soft-deleted")]
        public bool IsDeleted { get; set; } = false;
    }
}
