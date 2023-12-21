namespace NotificationManagement.Application.Factories.Notifications.Abstractions;
public interface INotificationStrategy
{
    Task<IActionResponse<string>> ExecuteAsync(string[] receivers, object message, CancellationToken cancellationToken);
}
