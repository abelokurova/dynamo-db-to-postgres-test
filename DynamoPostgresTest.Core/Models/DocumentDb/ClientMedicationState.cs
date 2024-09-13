
using System.ComponentModel.DataAnnotations;

namespace DynamoPostgresTest.WebApi;

public class StateDocument
{
    [Key]
    public Guid Id { get; set; }

    public string? DosageInstructions { get; set; }

    public string? FurtherInstructions { get; set; }
    public List<TestDocument> Dosages { get; set; }
}