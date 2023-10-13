namespace NotificationManagement.Application.Adapters;
public interface ISmsAdapter
{
    Task<string> SendAsync(string receiver, string message);
    Task<string> SendBulkAsync(string[] receiver, string message);
}
