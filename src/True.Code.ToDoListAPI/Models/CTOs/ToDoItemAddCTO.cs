using System.Text.Json.Serialization;


namespace True.Code.ToDoListAPI.Models.CTOs;

public class ToDoItemAddCTO
{
    [JsonPropertyName("title")] public string? Title { get; set; }

    [JsonPropertyName("description")] public string? Description { get; set; }

    [JsonPropertyName("iscompleted")] public bool? IsCompleted { get; set; }

    [JsonPropertyName("duedate")] public DateTime? DueDate { get; set; }

    [JsonPropertyName("priority")] public int? Level { get; set; }


    [JsonPropertyName("userId")] public int? UserId { get; set; }
}