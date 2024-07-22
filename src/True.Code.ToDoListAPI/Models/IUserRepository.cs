namespace True.Code.ToDoListAPI.Models;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAsync();
    
    Task<User> GetByIdAsync(int id);
    Task<IEnumerable<User>> GetAsync(string[] names);
    Task<User> AddAsync(User user);

    Task<IEnumerable<User>> AddRangeAsync(IEnumerable<User> users);
    Task<User> UpdateAsync(User user);

    Task<bool> DeleteAsync(User user);
}