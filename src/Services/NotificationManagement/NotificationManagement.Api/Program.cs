using NotificationManagement.Application;
using NotificationManagement.Infrastructure;
using Services.Common;

var builder = WebApplication.CreateBuilder(args);

#region Services
builder.AddServiceDefaults();
builder.Services.AddControllers();

builder.Services.AddApplicationServices()
                .AddInfrastructureServices(builder.Configuration);

#endregion

#region App
var app = builder.Build();
app.UseServiceDefaults();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();

#endregion
