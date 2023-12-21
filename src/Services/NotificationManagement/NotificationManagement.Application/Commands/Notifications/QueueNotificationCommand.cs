using MediatR;
using Services.Common;

namespace NotificationManagement.Application.Commands;

public record QueueNotificationCommand(int notificationTypeId, string[] receivers, string message) : IRequest<IActionResponse>
{
    public int NotificationTypeId { get; init; } = notificationTypeId;
    public string[] Receivers { get; init; } = receivers;
    public string Message { get; init; } = message;
}
