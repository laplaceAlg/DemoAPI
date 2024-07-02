using DomainLayer.Entities;

namespace ApplicationLayer.Contracts
{
    public interface IUser
    {
        Task<User> GetUserByIdAsync(int id);
        Task<int> CreateUserAsync(User user);
        Task<User> GetUserByUsernameAndPasswordAsync(string username, string password);
    }
}
