namespace True.Code.ToDoListAPI.Models;

public interface IPriorityRepository
{
    Task<IEnumerable<Priority>> Get();


    Task<Priority> Add(Priority priority);


    Task<IEnumerable<Priority>> AddRange(IEnumerable<Priority> priorities);
    Task<bool> Delete(Priority priority);
}