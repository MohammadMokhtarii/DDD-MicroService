// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using NotificationManagement.Domain.Contracts;

namespace NotificationManagement.Domain.Entities.NotificationAggregate;

public class NotificationAcitvity : Entity
{
    private int _notificationId;
    public Notification Notification { get; private set; }

    private int _notificationStatusId;
    public NotificationStatus NotificationStatus { get; private set; }

    private string _response;
    private DateTime _createdOn;

    public NotificationAcitvity(int notificationId, int notificationStatusId, string response)
    {
        ArgumentException.ThrowIfNullOrEmpty(response, nameof(response));

        _notificationId = notificationId;
        _notificationStatusId = notificationStatusId;
        _response = response;
        _createdOn = DateTime.Now;
    }
}
