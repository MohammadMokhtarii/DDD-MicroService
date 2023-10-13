// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using NotificationManagement.Domain.Contracts;

namespace NotificationManagement.Domain.Entities.NotificationAggregate;

public class NotificationStatus : Enumeration
{
    public static readonly NotificationStatus Pending = new(1, nameof(Pending));
    public static readonly NotificationStatus Successful = new(2, nameof(Successful));
    public static readonly NotificationStatus Failed = new(3, nameof(Failed));

    public NotificationStatus(int id, string name) : base(id, name)
    {
    }
}
