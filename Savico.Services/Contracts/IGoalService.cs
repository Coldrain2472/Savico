namespace Savico.Services.Contracts
{
    using Savico.Core.Models;
    using Savico.Core.Models.ViewModels.Goal;

    public interface IGoalService
    {
        decimal CalculateMonthlyContribution(Goal goal); // calculates monthly contribution

        Task UpdateGoalMonthlyContributionAsync(Goal goal); // updates monthly contribution

        Task<bool> UpdateGoalAsync(Goal updatedGoal); // indicates if the goal was updated or not

        Task AddGoalAsync(GoalInputViewModel model, string userId); // creates a goal

        Task<GoalViewModel> GetGoalByIdAsync(int goalId, string userId); // retrieves a goal by id

       // decimal CalculateMonthlySavings(decimal targetAmount, decimal currentAmount, DateTime targetDate); // calculates monthly savings

        Task DeleteGoalAsync(int goalId, string userId); // deletes a goal

        Task<IEnumerable<GoalViewModel>> GetAllGoalsAsync(string userId); // retrieves all goals
    }
}
