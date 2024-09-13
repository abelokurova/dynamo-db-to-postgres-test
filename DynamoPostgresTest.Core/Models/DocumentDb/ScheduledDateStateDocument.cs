using System.ComponentModel.DataAnnotations;

namespace DynamoPostgresTest.WebApi;

public class ScheduledDateStateDocument
{
    [Key]
    public Guid ScheduledDateId { get; set; }

    public DateTimeOffset? StartUtc { get; set; }

    public DateTimeOffset? EndUtc { get; set; }
}