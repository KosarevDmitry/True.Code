using Microsoft.AspNetCore.Components.Web;

namespace True.Code.ToDoListAPI.Models;

public interface IToDoItemRepository
{
    Task<ToDoItemCTO?> GetById(int id);

    Task<ToDoItem> Add(ToDoItem toDoItemCTO);

    Task<ToDoItem> Update(ToDoItemCTO toDoItem);

    Task<bool> Delete(int id);
}