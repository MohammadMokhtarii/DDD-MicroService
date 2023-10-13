﻿using MediatR;

namespace NotificationManagement.Application.Commands;

public record QueueNotificationCommand : IRequest<bool>
{
    public int NotificationTypeId { get; init; }
    public string[] Receivers { get; init; }
    public string Message { get; init; }

    public QueueNotificationCommand(int notificationTypeId, string[] receivers, string message)
    {
        NotificationTypeId = notificationTypeId;
        Receivers = receivers;
        Message = message;
    }
}