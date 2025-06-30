

using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);
var dataSourceBuilder = new NpgsqlDataSourceBuilder(builder.Configuration.GetConnectionString("DbConnectionString"));
dataSourceBuilder.UseNetTopologySuite();
var dataSource = dataSourceBuilder.Build();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
 builder.Services.AddDbContext<PostGresContext>(options =>     options.UseNpgsql(
    builder.Configuration.GetConnectionString("DbConnectionString")));
Console.WriteLine($"Connection string: {builder.Configuration.GetConnectionString("DbConnectionString")}" );
builder.Services.AddLogging(builder => builder.AddConsole());

var app = builder.Build();


app.UseAuthorization();



app.Run();

