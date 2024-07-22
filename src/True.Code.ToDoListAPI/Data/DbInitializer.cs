using True.Code.ToDoListAPI.Models;

namespace True.Code.ToDoListAPI.Data;

public static class DbInitializer
{
    public static void Initialize(this ToDoItemDbContext context)
    {
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        var users = new User[]
        {
            new User { Name = "Петр" },
            new User { Name = "Иван" },
            new User { Name = "Milosh" }
        };

        var priorities = new Priority[]
        {
            new Priority { Level = 1 },
            new Priority { Level = 2 },
            new Priority { Level = 3 },
        };

        var toDoItems = new ToDoItem[]
        {
            new ToDoItem()
            {
                Title = "Командировка",
                Description = "Что поделаешь - такая работа",
                DueDate = DateTime.Now.AddDays(1),
                IsCompleted = false,
                Level = 1,
                UserId = 1
            },
            new ToDoItem()
            {
                Title = "Квартальный отчет",
                Description = "Что поделаешь - такая работа",
                DueDate = DateTime.Now.AddDays(2),
                IsCompleted = false,
                Level = 2,
                UserId = 2
            },
            new ToDoItem()
            {
                Title = "Калькуляция",
                Description = "Что поделаешь - такая работа",
                DueDate = DateTime.Now.AddDays(3),
                IsCompleted = false,
                Level = 3,
                UserId = 3
            }
        };

        context.Users.AddRange(users);
        context.Priorities.AddRange(priorities);
        context.ToDoItems.AddRange(toDoItems);
        context.SaveChanges();
    }
}