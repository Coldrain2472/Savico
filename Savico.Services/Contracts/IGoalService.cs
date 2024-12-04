namespace Savico.Services.Contracts
{
    using Savico.Core.Models.ViewModels.Goal;

    public interface IGoalService
    {
        Task<GoalInputViewModel> GetGoalForEditAsync(int goalId, string userId); // retrieves a goal for edit

        Task<IEnumerable<GoalViewModel>> GetAllGoalsAsync(string userId); // retrieves all goals

        Task<GoalContributeViewModel> GetGoalContributeViewModelAsync(int goalId, string userId); // retrieves the goal contribute view model

        Task ContributeToGoalAsync(GoalContributeViewModel model, string userId); // contributes to a goal

        Task<GoalInputViewModel> GetGoalInputViewModelAsync(string userId); // retrieves the goal input view model

        Task AddGoalAsync(GoalInputViewModel model, string userId); // adds a goal

        Task<GoalViewModel> GetGoalByIdAsync(int id, string userId); // retrieves a goal by id

        Task UpdateGoalAsync(int id, GoalInputViewModel model, string userId); // updates a goal

        Task DeleteGoalAsync(int id, string userId); // deletes a goal
    }
}
