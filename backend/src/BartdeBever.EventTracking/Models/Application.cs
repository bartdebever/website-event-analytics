using System.ComponentModel.DataAnnotations.Schema;

namespace BartdeBever.EventTracking.Models;

public class Application
{
    [Column("id")]
    public Guid Id { get; set; }
    
    [Column("name")]
    public string Name { get; set; }
    
    [Column("api_key")]
    public string ApiKey { get; set; }
    
    [Column("schema_name")]
    public string SchemaName { get; set; }
}