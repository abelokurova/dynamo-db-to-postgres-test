using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DynamoPostgresTest.WebApi;

[PrimaryKey(nameof(EntityGuid), nameof(VersionId))]
public abstract class AuditableHistory
{
    public Guid EntityGuid { get; set; }

    public long VersionId { get; set; }

    public Guid VersionGuid { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }

    public string ChangeComment { get; set; }
}