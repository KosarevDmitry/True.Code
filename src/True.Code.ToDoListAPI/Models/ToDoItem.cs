using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace True.Code.ToDoListAPI.Models;

public class ToDoItem
{
    [Key] [Column(Order = 0)] public int Id { get; set; }

    [MaxLength(255)]
    [Required]
    [Column(Order = 1)]
    public string? Title { get; set; }

    [MaxLength(255)] [Column(Order = 2)] public string? Description { get; set; }

    [Column(Order = 3)] public bool? IsCompleted { get; set; }
    [Column(Order = 4)] public DateTime? DueDate { get; set; }

    [Column(Order = 5)] public int? Level { get; set; }
    public Priority? Priority { get; set; }


    [Column(Order = 6)] public int? UserId { get; set; }
    public User? User { get; set; }

    [Column(Order = 7)] public DateTime Created { get; set; }
}