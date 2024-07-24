using Microsoft.EntityFrameworkCore;

namespace True.Code.ToDoListAPI.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ToDoItemDbContext _context;

    public UserRepository(ToDoItemDbContext context)
    {
        _context = context;
    }


    public async Task<IEnumerable<User>> GetAsync()
    {
        var projects = _context.Users.AsQueryable();

        return await projects.ToListAsync();
    }

    
    public async Task<User?> GetByIdAsync(int id)
    {
       var user =  await _context.Users.SingleOrDefaultAsync(x => x.Id == id);
     
       return user;
    }

    public async Task<IEnumerable<User>> GetAsync(string[] names)
    {
        var projects = _context.Users.AsQueryable();

        if (names != null && names.Any())
            projects = projects.Where(x => names.Contains(x.Name));

        return await projects.ToListAsync();
    }


    public async Task<User> AddAsync(User user)
    {
        
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<IEnumerable<User>> AddRangeAsync(IEnumerable<User> users)
    {
        await _context.Users.AddRangeAsync(users);
        await _context.SaveChangesAsync();
        return users;
    }

    public async Task<User> UpdateAsync(User user)
    {
        var projectForChanges = await _context.Users.SingleAsync(x => x.Id == user.Id);
        projectForChanges.Name = user.Name;
        _context.Users.Update(projectForChanges);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<bool> DeleteAsync(User user)
    {
        int result = await _context.Users.Where(x => x.Id == user.Id).ExecuteDeleteAsync();
        if (result != 0) return true;

        return false;
    }
}