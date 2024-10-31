namespace Savico.Infrastructure.Data.DataSeeding
{
    using Savico.Core.Models;

    public class DataSeed
    {
        public DataSeed()
        {
            
        }

        // Users


        // Budgets
        public Budget BudgetOne { get; set; } = null!;

        public Budget BudgetTwo { get; set; } = null!;

        public Budget BudgetThree { get; set; } = null!;

        // Expenses

        public Expense ExpenseOne {  get; set; } = null!;

        public Expense ExpenseTwo {  get; set; } = null!;

        public Expense ExpenseThree {  get; set; } = null!;

        // Incomes

        public Income IncomeONe {  get; set; } = null!;

        public Income IncomeTwo {  get; set; } = null!;

        public Income IncomeThree {  get; set; } = null!;

        // Goals

        public Goal GoalOne {  get; set; } = null!;

        public Goal GoalTwo {  get; set; } = null!;

        public Goal GoalThree {  get; set; } = null!;

        private void SeedUsers()
        {

        }

        private void SeedBudgets()
        {
            //BudgetOne = new Budget()
            //{
            //    Id = 1,
            //    UserId = "",
            //    User = ,
            //    StartDate = ,
            //    EndDate = ,
            //    TotalAmount = ,
            //    Expenses = 
            //}
        }

        private void SeedExpenses()
        {

        }

        private void SeedIncomes()
        {

        }

        private void SeedGoals()
        {

        }
    }
}
