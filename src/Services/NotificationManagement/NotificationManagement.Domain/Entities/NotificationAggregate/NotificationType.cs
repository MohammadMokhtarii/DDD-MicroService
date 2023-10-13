// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using NotificationManagement.Domain.Contracts;

namespace NotificationManagement.Domain.Entities.NotificationAggregate;

public class NotificationType: Enumeration
{
    public static readonly NotificationType Sms = new(1, nameof(Sms));
    public static readonly NotificationType Email = new(2, nameof(Email));
    public static readonly NotificationType Push = new(3, nameof(Push));

    public NotificationType(int id, string name) : base(id, name)
    {
    }
}
