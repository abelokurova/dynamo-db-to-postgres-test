

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.Diagnostics.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Logging;

namespace DynamoPostgresTest.WebApi;

[Route("api/[controller]")]
[ApiController]
public class DocumentController : ControllerBase
{
  private readonly DataContext _context;
  private readonly IManagedTracer _tracer;

  private readonly ILogger<DocumentController> _logger;

  public DocumentController(DataContext context, IManagedTracer tracer, ILogger<DocumentController> logger)
  {
    _context = context;
    _tracer = tracer;
    _logger = logger;
  }

  // GET: api/Documents/5
  [HttpGet("{guid}")]
  public async Task<ActionResult<HistoryDocument>> GetCeraRecord(Guid guid)
  {
    try
    {
      var document = await _context.Documents.FindAsync(guid);

      if (document == null)
      {
        return NotFound();
      }

      return document;
    }
    catch (Exception ex)
    {
      _logger.LogError(ex.Message);
      return BadRequest(ex.Message);
    }
  }


  // GET: api/trace
  [HttpGet]
  public async Task<ActionResult<int>> RunClientMedicationHistoryDocumentsTracing()
  {
    try
    {
      var runnerJob = new MetricsGatheringJob(_context, _tracer);
      return await runnerJob.RunTracing();
    }
    catch (Exception ex)
    {
      _logger.LogError(ex.Message);
      return BadRequest(ex.Message);
    }
  }
}