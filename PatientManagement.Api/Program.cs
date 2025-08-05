using PatientManagement.Infrastructure;
using PatientManagement.Core;
using PatientManagement.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddInfrastructure();
builder.Services.AddCore();

// Add controllers to the service collection
builder.Services.AddControllers();

// Build the web application
var app = builder.Build();

app.UseExceptionHandlingMiddleware();
//Routing
app.UseRouting();

// Auth
app.UseAuthentication();
app.UseAuthorization();

// Controller routes
app.MapControllers();

app.Run();
