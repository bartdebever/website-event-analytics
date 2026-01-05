using System.ComponentModel.DataAnnotations.Schema;

namespace BartdeBever.EventTracking.Models;

public class EventLog
{
    [Column("id")]
    public long Id { get; set; }
    
    [Column("event_name")]
    public string EventName { get; set; }
    
    [Column("session_id")]
    public Guid SessionId { get; set; }
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
    
    [Column("url")]
    public string Url { get; set; }
    
    [Column("data")]
    public object Data { get; set; }
}