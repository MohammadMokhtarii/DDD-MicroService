// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using NotificationManagement.Domain.Entities.NotificationAggregate;

namespace NotificationManagement.Domain.Contracts;
public interface IUnitofWork
{
    public Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);

    public INotificationRepository NotificationRepo { get; }
}
