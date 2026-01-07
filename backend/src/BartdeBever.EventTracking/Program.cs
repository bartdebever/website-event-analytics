using System.Text.Json;
using BartdeBever.EventTracking.Contexts;
using BartdeBever.EventTracking.Filters;
using BartdeBever.EventTracking.Repositories;
using BartdeBever.EventTracking.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configure database context with Postgres
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<EventTrackingDbContext>(options =>
    options.UseNpgsql(connectionString));

// Register repositories and services
builder.Services.AddScoped<IEventLogRepository, EventLogRepository>();
builder.Services.AddScoped<ApplicationRepository>();
builder.Services.AddScoped<IEventService, EventService>();

// Register filters
builder.Services.AddScoped<ApiKeyAuthenticationFilter>();

// Add Application Insights if configured
var appInsightsConnectionString = builder.Configuration["ApplicationInsights:ConnectionString"];
if (!string.IsNullOrEmpty(appInsightsConnectionString))
{
    builder.Services.AddApplicationInsightsTelemetry(options =>
    {
        options.ConnectionString = appInsightsConnectionString;
    });
}

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Configure JSON serialization for proper handling of the Data property
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
