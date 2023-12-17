namespace NotificationManagement.Api.Core;

public class ApplicationInitialize : BackgroundService
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly ILogger<ApplicationInitialize> _logger;
    public ApplicationInitialize(IServiceScopeFactory serviceScopeFactory)
    {
        ArgumentNullException.ThrowIfNull(nameof(serviceScopeFactory));

        var serviceProvider = serviceScopeFactory.CreateScope();

        _webHostEnvironment = serviceProvider.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
        _logger = serviceProvider.ServiceProvider.GetRequiredService<ILogger<ApplicationInitialize>>();

    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (_webHostEnvironment.IsDevelopment())
        {

        }
    }
}
