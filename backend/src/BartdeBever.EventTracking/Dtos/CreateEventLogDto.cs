using System.ComponentModel.DataAnnotations;

namespace BartdeBever.EventTracking.Dtos;

public class CreateEventLogDto
{
    /// <summary>
    /// The name of the event being tracked
    /// </summary>
    [Required]
    [MaxLength(255)]
    public string EventName { get; set; } = string.Empty;

    /// <summary>
    /// The identifier for the current session.
    /// </summary>
    [Required]
    public Guid SessionId { get; set; }

    /// <summary>
    /// The URL where the event occurred.
    /// </summary>
    [MaxLength(2048)]
    public string? Url { get; set; }

    /// <summary>
    /// Additional data associated with the event (will be stored as JSON).
    /// </summary>
    public object? Data { get; set; }
}
