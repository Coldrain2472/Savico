namespace Savico.Services.Contracts
{
    using Savico.Core.Models.ViewModels.Goal;

    public interface IGoalService
    {
        Task<GoalInputViewModel> GetGoalForEditAsync(int goalId, string userId);

        Task<IEnumerable<GoalViewModel>> GetAllGoalsAsync(string userId);

        Task<GoalContributeViewModel> GetGoalContributeViewModelAsync(int goalId, string userId);

        Task ContributeToGoalAsync(GoalContributeViewModel model, string userId);

        Task<GoalInputViewModel> GetGoalInputViewModelAsync(string userId);

        Task AddGoalAsync(GoalInputViewModel model, string userId);

        Task<GoalViewModel> GetGoalByIdAsync(int id, string userId);

        Task UpdateGoalAsync(int id, GoalInputViewModel model, string userId);

        Task DeleteGoalAsync(int id, string userId);
    }
}
