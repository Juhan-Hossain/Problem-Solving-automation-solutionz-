using Automations_solutionz.Entity;

namespace Automations_solutionz.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUserAsync();
    }
}
