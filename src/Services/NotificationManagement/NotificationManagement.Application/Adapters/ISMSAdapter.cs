namespace NotificationManagement.Application.Adapters;
public interface ISMSAdapter
{

    Task SendAsync(string receiver, string message);
    Task SendBulkAsync(string[] receiver, string message);
}
