using Microsoft.EntityFrameworkCore;

namespace True.Code.ToDoListAPI.Infrastructure.Repositories;

public class PriorityRepository : IPriorityRepository
{
    private readonly ToDoItemDbContext _context;

    public PriorityRepository(ToDoItemDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Priority>> Get()
    {
        var projects = _context.Priorities.AsQueryable();

        return await projects.ToListAsync();
    }

    public async Task<Priority> Add(Priority priority)
    {
        await _context.Priorities.AddAsync(priority);
        await _context.SaveChangesAsync();

        return priority;
    }

    public async Task<IEnumerable<Priority>> AddRange(IEnumerable<Priority> priorities)
    {
        await _context.Priorities.AddRangeAsync(priorities);
        await _context.SaveChangesAsync();

        return priorities;
    }

    public async Task<bool> Delete(Priority priority)
    {
        var result = await _context.Priorities.Where(p => p.Level == priority.Level).ExecuteDeleteAsync();
        if (result != 0) return true;

        return false;
    }
}