using DynamoPostgresTest.WebApi;
using Google.Cloud.Diagnostics.AspNetCore;
using Google.Cloud.Diagnostics.Common;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Npgsql;

NpgsqlConnection.GlobalTypeMapper.EnableDynamicJson();

const string projectId = "gcp-id";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = builder.Configuration;

builder.Services.AddDbContext<DataContext>();

builder.Services.AddGoogleTraceForAspNetCore(new AspNetCoreTraceOptions
{
    ServiceOptions = new Google.Cloud.Diagnostics.Common.TraceServiceOptions
    {
        ProjectId = projectId
    }
});

builder.Services.AddLogging(
    builder => builder.AddGoogle(new LoggingServiceOptions
    {
        ProjectId = projectId,
        ServiceName = "DynamoDbPostgresPoC",
        Version = "0.0.1"
    })
);

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.MapControllers();
app.Run();


