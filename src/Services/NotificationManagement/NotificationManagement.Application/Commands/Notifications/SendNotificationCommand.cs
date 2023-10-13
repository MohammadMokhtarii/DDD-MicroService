using MediatR;

namespace NotificationManagement.Application.Commands.Notifications;

public record SendNotificationCommand : IRequest<bool>
{
    public int NotificationTypeId { get; init; }
    public string Receiver { get; init; }
    public string Message { get; init; }

    public SendNotificationCommand(int notificationTypeId, string receiver, string message)
    {
        NotificationTypeId = notificationTypeId;
        Receiver = receiver;
        Message = message;
    }
}
