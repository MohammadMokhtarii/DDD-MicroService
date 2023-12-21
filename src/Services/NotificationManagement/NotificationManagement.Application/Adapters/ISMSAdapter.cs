namespace NotificationManagement.Application.Adapters;
public interface ISmsAdapter
{
    Task<IActionResponse<string>> SendAsync(string receiver, string message);
    Task<IActionResponse<string>> SendBulkAsync(string[] receiver, string message);
}
