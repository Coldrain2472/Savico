using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savico.Core.Models
{
    public class Goal
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Target amount must be greater than zero.")]
        public decimal TargetAmount { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Current amount must be greater than or equal to zero.")]
        public decimal CurrentAmount { get; set; }

        [Required]
        public DateTime TargetDate { get; set; }
    }
}
