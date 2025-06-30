
using Microsoft.AspNetCore.HttpOverrides;
using smartparking.db.postgres;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// builder.Services.AddScoped<DbConnectionOptions>();

builder.Services.AddOptions<DbConnectionOptions>().BindConfiguration(nameof(DbConnectionOptions));
builder.Services.AddScoped<PostGresClient>();

// builder.Services.AddOptions<DbConnectionOptionsOptions>().BindConfiguration(nameof(DbConnectionOptionsOptions));
var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {

app.UsePathBase("/ParkingArea");

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/ParkingArea/swagger/v1/swagger.json", "My API V1");
});

// }
// else
// {
//     app.UseSwagger( options =>
//     {
//         options.
//     });
//     app.UseSwaggerUI();
//  }

app.UseAuthorization();

app.MapControllers();


app.Run();

