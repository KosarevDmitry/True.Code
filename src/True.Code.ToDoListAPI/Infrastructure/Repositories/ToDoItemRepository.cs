using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace True.Code.ToDoListAPI.Infrastructure.Repositories;

public class ToDoItemRepository : IToDoItemRepository
{
    private readonly ToDoItemDbContext _context;

    public ToDoItemRepository(ToDoItemDbContext testProjectContext)
    {
        _context = testProjectContext;
    }

    public async Task<ToDoItemCTO?> GetById(int id)
    {
        var query = await (from d in _context.Set<ToDoItem>()
            join u in _context.Set<User>()
                on d.UserId equals u.Id
            where d.Id == id
            select new ToDoItemCTO()
            {
                Id = d.Id,
                Description = d.Description,
                Title = d.Title,
                User = u.Name,
                UserId = u.Id,
                Level = d.Level,
                DueDate = d.DueDate,
                IsCompleted = d.IsCompleted,
                Created = d.Created
            }).FirstOrDefaultAsync();

        return query;
    }

    private async Task<bool> IsExist(int id)
    {
        var item = await _context.ToDoItems.SingleOrDefaultAsync(x => x.Id == id);
        if (item is not null) return true;
        return false;
    }

    public async Task<ToDoItem> Add(ToDoItem toDoItem)
    {
        await _context.ToDoItems.AddAsync(toDoItem);
        await _context.SaveChangesAsync();
        return toDoItem;
    }

    public async Task<ToDoItem> Update(ToDoItemCTO toDoItem)
    {
        var toDoItemForChanges = await _context.ToDoItems.SingleAsync(x => x.Id == toDoItem.Id);
        toDoItemForChanges.Title = toDoItem.Title;
        toDoItemForChanges.Description = toDoItem.Description;
        toDoItemForChanges.IsCompleted = toDoItem.IsCompleted;
        toDoItemForChanges.Level = toDoItem.Level;
        toDoItemForChanges.UserId = toDoItem.UserId;
        toDoItemForChanges.DueDate = toDoItem.DueDate;

        _context.ToDoItems.Update(toDoItemForChanges);
        await _context.SaveChangesAsync();

        return toDoItemForChanges;
    }


    public async Task<bool> Delete(int id)
    {
        var result = await _context.ToDoItems.Where(e => e.Id == id).ExecuteDeleteAsync();
        if (result != 0) return true;

        return false;
    }
}
