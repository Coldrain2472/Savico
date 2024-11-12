namespace Savico.Services.Contracts
{
    using Savico.Core.Models;
    using Savico.Core.Models.ViewModels.Goal;

    public interface IGoalService
    {
        decimal CalculateMonthlyContribution(Goal goal); // calculates monthly contribution

		decimal CalculateMonthlyGoalContribution(string userId, Goal goal); // calculates monthly goal contribution

		Task UpdateGoalMonthlyContributionAsync(Goal goal); // updates monthly contribution

        Task AddGoalAsync(GoalInputViewModel model, string userId); // creates a goal

		Task<GoalViewModel> GetGoalByIdAsync(int goalId, string userId); // retrieves a goal by id

        Task UpdateGoalAsync(int goalId, GoalInputViewModel model, string userId); // updates the goal

        Task DeleteGoalAsync(int goalId, string userId); // deletes a goal

        Task<IEnumerable<GoalViewModel>> GetAllGoalsAsync(string userId); // retrieves all goals

        Task<GoalInputViewModel> GetGoalForEditAsync(int goalId, string userId); // prepares the goal model for edit

        Task MarkGoalAsAchievedAsync(int goalId, string userId); // marks a goal as "Achieved"

        Task ContributeToGoalAsync(int goalId, string userId, decimal contributionAmount); // contributes money to the goal

        Task<GoalInputViewModel> GetGoalInputViewModelAsync(string userId); // helps for the creation of a goal+currency
    }
}
