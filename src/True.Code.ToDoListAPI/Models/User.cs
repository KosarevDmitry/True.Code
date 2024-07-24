using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace True.Code.ToDoListAPI.Models;

public class User
{
    [Key] [Column(Order = 0)] 
    
    public int Id { get; set; }

    [MaxLength(100)]
    [Required]
    [Column(Order = 1)]
    public string? Name { get; set; }
   
    [JsonIgnore]
    public ICollection<ToDoItem> ToDoItems { get; } = new List<ToDoItem>();
}