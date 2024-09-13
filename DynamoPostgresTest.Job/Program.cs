// See https://aka.ms/new-console-template for more information
using DynamoPostgresTest.WebApi;
using Google.Cloud.Diagnostics.Common;
using Google.Cloud.Trace.V1;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Npgsql;

public partial class Program
{

  public static void Main(string[] args)
  {
    NpgsqlConnection.GlobalTypeMapper.EnableDynamicJson();
  }
} 