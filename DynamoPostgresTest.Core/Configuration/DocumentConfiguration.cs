using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DynamoPostgresTest.WebApi;

public class DocumentConfiguration : IEntityTypeConfiguration<HistoryDocument>
{
  public void Configure(EntityTypeBuilder<HistoryDocument> builder)
  {

    var guidToStringConverter = new ValueConverter<Guid, string>(
    v => v.ToString(),
    v => Guid.Parse(v));

    builder.Property(e => e.EntityGuid)
    .HasConversion(guidToStringConverter)
    .HasColumnType("text");

    builder.Property(e => e.VersionGuid)
    .HasConversion(guidToStringConverter)
    .HasColumnType("text");

    builder.Property(e => e.CreatedBy)
    .HasConversion(guidToStringConverter)
    .HasColumnType("text");

    var datetimeoffsetToStringConverter = new ValueConverter<DateTimeOffset, string>(
    v => v.ToString(),
    v => DateTimeOffset.Parse(v));

    builder.Property(e => e.CreatedAt)
    .HasConversion(datetimeoffsetToStringConverter)
    .HasColumnType("text");
  }
}

