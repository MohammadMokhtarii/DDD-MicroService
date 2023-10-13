// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using NotificationManagement.Domain.Contracts;
using NotificationManagement.Domain.Entities.ReceiverAggregate;
using NotificationManagement.Domain.Events;
using NotificationManagement.Domain.Exceptions;

namespace NotificationManagement.Domain.Entities.NotificationAggregate;
public class Notification : Entity, IAggregateRoot
{
    public string Receiver { get; private set; }
    public string Message { get; private set; }

    private int _retryCount;
    private bool _read;

    private List<NotificationAcitvity> _notificationAcitvities;
    public IEnumerable<NotificationAcitvity> NotificationAcitvities => _notificationAcitvities.AsReadOnly();

    private int _notificationTypeId;
    public NotificationType NotificationType { get; private set; }

    private int _notificationPriorityId;
    public NotificationPriority NotificationPriority { get; private set; }

    private int _notificationStatusId;
    public NotificationStatus NotificationStatus { get; private set; }

    private int? _receiverInfoId;
    public ReceiverInfo ReceiverInfo { get; private set; }

    public Notification(string receiver, string message, int notificationTypeId)
    {
        ArgumentException.ThrowIfNullOrEmpty(receiver, nameof(receiver));
        ArgumentException.ThrowIfNullOrEmpty(message, nameof(message));

        Receiver = receiver;
        Message = message;
        _notificationTypeId = notificationTypeId;
        _notificationStatusId = NotificationStatus.Pending.Id;
        _notificationPriorityId = NotificationPriority.High.Id;

    }
    public Notification(string receiver, string message, int notificationTypeId, int notificationPriorityId) : this(receiver, message, notificationTypeId)
    {
        _notificationPriorityId = notificationPriorityId;
    }
    public void ChangeStatus(int notificationStatusId, string response)
    {
        ArgumentException.ThrowIfNullOrEmpty(response, nameof(response));

        if (_notificationStatusId == NotificationStatus.Successful.Id)
            throw new NotificationDomainException("Already Successed");

        AddDomainEvent(new NotificationStatusChangedDomainEvent(Id, _notificationStatusId, notificationStatusId));

        _notificationStatusId = notificationStatusId;
        _retryCount++;
        _notificationAcitvities.Add(new(Id, _notificationStatusId, response));
    }

    public void MarkRead()
    {
        if (_notificationStatusId != NotificationStatus.Successful.Id)
            throw new NotificationDomainException("Invalid status for marking read flag");

        _read = true;

        AddDomainEvent(new NotificationReadDomainEvent(Id));
    }
}
