

using System.ComponentModel.DataAnnotations.Schema;

namespace DynamoPostgresTest.WebApi;


[Table("history-document")]
public class HistoryDocument : AuditableHistory
{
    //should be jsonb field
    public StateDocument Medication { get; set; } = null!;
    public List<Change>? Changes { get; set; }
}