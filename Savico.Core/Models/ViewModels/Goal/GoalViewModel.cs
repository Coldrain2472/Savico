﻿namespace Savico.Core.Models.ViewModels.Goal
{
    public class GoalViewModel
    {
        public int Id { get; set; }

        public decimal TargetAmount { get; set; }

        public decimal CurrentAmount { get; set; }

        public DateTime TargetDate { get; set; }

        public string? Currency {  get; set; }

       // public decimal MonthlyContribution { get; set; }

        public decimal MonthlyGoalContribution { get; set; }

        public string? Description { get; set; }
    }
}
