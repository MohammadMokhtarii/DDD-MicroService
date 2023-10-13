// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;

namespace NotificationManagement.Domain.Events;
public class NotificationReadDomainEvent : INotification
{
    public int NotificationId { get; }

    public NotificationReadDomainEvent(int notificationId)
    {
        NotificationId = notificationId;
    }
}
