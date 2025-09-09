using System.Data;
using Microsoft.Data.SqlClient;
using SqlDapperDemo.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// IDbConnection for Dapper using Aspire-provided connection string
builder.Services.AddScoped<IDbConnection>(sp =>
{
    var cs = sp.GetRequiredService<IConfiguration>()
        .GetConnectionString("sql-dapper-demo");
    if (string.IsNullOrWhiteSpace(cs))
        throw new InvalidOperationException("Missing connection string 'sql-dapper-demo'.");
    return new SqlConnection(cs);
});

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Map data endpoints
app.MapCaptainEndpoints();
app.MapFactionEndpoints();
app.MapStarshipClassEndpoints();
app.MapStarshipEndpoints();
app.MapStarshipCaptainEndpoints();

app.Run();
