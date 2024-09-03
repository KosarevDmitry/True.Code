using System.Text.Json.Serialization;


namespace True.Code.ToDoListAPI.Models.CTOs;

public class ToDoItemAddCTO
{
    [JsonPropertyName("title")] public string? Title { get; init; }

    [JsonPropertyName("description")] public string? Description { get; init; }

    [JsonPropertyName("iscompleted")] public bool? IsCompleted { get; init; }

    [JsonPropertyName("duedate")] public DateTime? DueDate { get; init; }

    [JsonPropertyName("priority")] public int? Level { get; init; }


    [JsonPropertyName("userId")] public int? UserId { get; init; }
}
