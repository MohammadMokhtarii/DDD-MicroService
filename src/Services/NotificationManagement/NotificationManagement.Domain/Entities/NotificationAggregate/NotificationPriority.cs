// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using NotificationManagement.Domain.Contracts;

namespace NotificationManagement.Domain.Entities.NotificationAggregate;

public class NotificationPriority : Enumeration
{
    public static readonly NotificationPriority High = new(1, nameof(High));
    public static readonly NotificationPriority Medium = new(2, nameof(Medium));
    public static readonly NotificationPriority Low = new(3, nameof(Low));

    public NotificationPriority(int id, string name) : base(id, name)
    {
    }
}
