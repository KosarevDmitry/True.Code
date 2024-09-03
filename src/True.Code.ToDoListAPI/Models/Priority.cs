using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace True.Code.ToDoListAPI.Models;

public class Priority : IPriority
{
    [Range(0, 100)] [Key] public int Level { get; set; }
    public ICollection<ToDoItem> ToDoItems { get; } = new List<ToDoItem>();
}
