
using System.ComponentModel.DataAnnotations;

namespace DynamoPostgresTest.WebApi;

public class TestDocument
{
    [Key]
    public Guid Id { get; set; }

    public string? Dosage { get; set; }
    public DateTimeOffset? DosageEndUtc { get; set; }
    
    public List<ScheduledDateStateDocument>? ScheduledDates { get; set; }
}