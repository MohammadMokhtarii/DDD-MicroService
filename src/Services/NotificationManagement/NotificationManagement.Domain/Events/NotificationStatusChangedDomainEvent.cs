// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;

namespace NotificationManagement.Domain.Events;
public class NotificationStatusChangedDomainEvent : INotification
{
    public int NotificationId { get; }
    public int PrevNotificationStatusId { get; }
    public int NotificationStatusId { get; }

    public NotificationStatusChangedDomainEvent(int notificationId, int prevNotificationStatusId, int notificationStatusId)
    {
        NotificationId = notificationId;
        NotificationStatusId = notificationStatusId;
        PrevNotificationStatusId = prevNotificationStatusId;
    }
}
