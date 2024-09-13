namespace DynamoPostgresTest.WebApi;

public class Change
{
    public Operation Operation { get; set; }
    public string Name { get; set; } = null!;
}