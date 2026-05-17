using Microsoft.OpenApi;
using Restaurant.Presentation;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Restaurant.Integration.Test")]

var builder = WebApplication.CreateBuilder(args);

// =======================================================
// 1) Configure Services (IOC Container)
// =======================================================

// -------------------------------------------------------
// Logging
// -------------------------------------------------------
builder.Services.AddLogging();

// -------------------------------------------------------
// Controllers
// -------------------------------------------------------
builder.Services.AddControllers();

// -------------------------------------------------------
// OpenAPI (.NET 8–10 built-in)
// -------------------------------------------------------
builder.Services.AddOpenApi(); // exposes /openapi/v1.json

// Swagger (Swashbuckle - existing setup)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger(); 

// -------------------------------------------------------
// API Versioning
// -------------------------------------------------------
// TODO: Add API Versioning if needed

// -------------------------------------------------------
// Health Checks
// -------------------------------------------------------
// TODO: builder.Services.AddHealthChecks();

// -------------------------------------------------------
// CORS
// -------------------------------------------------------
// TODO: Configure CORS policy

// -------------------------------------------------------
// Clean Architecture - DI Registrations
// -------------------------------------------------------
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplications();
builder.Services.AddRepositories();
builder.Services.AddMappers();

// -------------------------------------------------------
// Http Client
// -------------------------------------------------------
builder.Services.AddHttpClient();


var app = builder.Build();

// =======================================================
// 2) Configure Middleware Pipeline
// =======================================================

// -------------------------------------------------------
// Global Exception Handling
// -------------------------------------------------------
app.UseMiddleware<ExceptionHandlingMiddleware>();

// -------------------------------------------------------
// HSTS (Production Only)
// -------------------------------------------------------
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

// -------------------------------------------------------
// HTTPS Redirection
// -------------------------------------------------------
app.UseHttpsRedirection();

// -------------------------------------------------------
// CORS
// -------------------------------------------------------
// TODO: app.UseCors("Default");

// -------------------------------------------------------
// Routing
// -------------------------------------------------------
app.UseRouting();

// -------------------------------------------------------
// Authorization
// -------------------------------------------------------
app.UseAuthorization();


// =======================================================
// 3) OpenAPI & Swagger
// =======================================================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Built-in OpenAPI endpoint (available in all environments)
app.MapOpenApi();


// =======================================================
// 4) Endpoint Mapping
// =======================================================

// Controllers
app.MapControllers();

// Health Checks
// TODO: app.MapHealthChecks("/health");

// Optional root endpoint
// TODO: app.MapGet("/", () => "Restaurant API is running...");


// =======================================================
// 5) Run Application
// =======================================================

app.Run();


// Required for Integration Testing
public partial class Program { }
