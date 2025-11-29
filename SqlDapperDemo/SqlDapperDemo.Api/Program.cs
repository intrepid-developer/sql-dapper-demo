using SqlDapperDemo.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// SqlConnection for Dapper using Aspire-provided connection string
builder.AddSqlServerClient(connectionName: "sql-dapper-demo");

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
}

// Only use HTTPS redirection if not in Testing environment
if (!app.Environment.IsEnvironment("Testing"))
{
  app.UseHttpsRedirection();
}

// Map data endpoints
app.MapCaptainEndpoints();
app.MapFactionEndpoints();
app.MapStarshipClassEndpoints();
app.MapStarshipEndpoints();
app.MapStarshipCaptainEndpoints();

app.Run();
