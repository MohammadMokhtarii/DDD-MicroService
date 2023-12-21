using MediatR;

namespace NotificationManagement.Application.Commands;

public record SendNotificationCommand(int notificationTypeId, string receiver, string message) : IRequest<IActionResponse<string>>
{
    public int NotificationTypeId { get; init; } = notificationTypeId;
    public string Receiver { get; init; } = receiver;
    public string Message { get; init; } = message;

}
