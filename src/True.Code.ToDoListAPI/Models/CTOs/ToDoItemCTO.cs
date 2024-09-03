using System.Text.Json.Serialization;

namespace True.Code.ToDoListAPI.Models.CTOs;

public class ToDoItemCTO
{
    [JsonPropertyName("id")]
    [JsonRequired]
    public int Id { get; init; }

    [JsonPropertyName("title")] public string? Title { get; init; }

    [JsonPropertyName("description")] public string? Description { get; init; }

    [JsonPropertyName("iscompleted")] public bool? IsCompleted { get; init; }

    [JsonPropertyName("duedate")] public DateTime? DueDate { get; init; }

    [JsonPropertyName("priority")] public int? Level { get; init; }

    [JsonPropertyName("userId")] public int? UserId { get; init; }

    [JsonPropertyName("user")] public string? User { get; init; }

    [JsonPropertyName("created")] public DateTime? Created { get; init; }
}
