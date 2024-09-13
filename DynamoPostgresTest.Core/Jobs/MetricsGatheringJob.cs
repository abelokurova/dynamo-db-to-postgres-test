using Google.Cloud.Diagnostics.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DynamoPostgresTest.WebApi;

public class MetricsGatheringJob
{
  private readonly DataContext _context;
  private readonly IManagedTracer _tracer;

  public MetricsGatheringJob(DataContext context, IManagedTracer tracer)
  {
    _context = context;
    _tracer = tracer;
  }

  public async Task<int> RunTracing()
  {
    var tasksCompleted = 0;
    var documents = new List<HistoryDocument>();

    //get all items
    using (_tracer.StartSpan("PgSQL/DynamoDB: Get all records"))
    {
      documents = await _context.Documents.ToListAsync();
    }
    tasksCompleted++;

    //get all items for the certain close
    using (_tracer.StartSpan("PgSQL/DynamoDB: Get all records which match the condition"))
    {
      documents = await _context.Documents.Where(doc => doc.Medication.DosageInstructions != null
                                                     && doc.Medication.Dosages.Count > 1).ToListAsync();
    }

    tasksCompleted++;

    // batch update set of items simple
    using (_tracer.StartSpan("PgSQL/DynamoDB: Update a field inside jsonb in the set of entities simple"))
    {
      foreach (var document in documents)
      {
        document.Medication.DosageInstructions = "updated dosage instruction";
      }

      _context.Documents.UpdateRange(documents);

      _context.SaveChanges();

    }

    tasksCompleted++;

    // batch update set of items advanced
    using (_tracer.StartSpan("PgSQL/DynamoDB: Update a field inside jsonb in the set of entities advanced"))
    {
      foreach (var document in documents)
      {
        foreach (var dosage in document.Medication.Dosages)
        {
          dosage.DosageEndUtc = DateTime.UtcNow;
          var newScheduledDate = new ScheduledDateStateDocument
          {
            ScheduledDateId = Guid.NewGuid(),
            StartUtc = DateTime.UtcNow,
            EndUtc = DateTime.UtcNow
          };
          dosage.ScheduledDates?.Add(newScheduledDate);
        }
        document.Medication.DosageInstructions = "updated dosage instruction";
      }

      _context.Documents.UpdateRange(documents);
      _context.SaveChanges();
    }

    tasksCompleted++;

    return tasksCompleted;
  }


}
