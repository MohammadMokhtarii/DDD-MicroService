// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using NotificationManagement.Domain.Contracts;

namespace NotificationManagement.Domain.Entities.NotificationAggregate;
public interface INotificationRepository : IRepository<Notification>
{
    Notification Add(Notification notification);
    void AddRange(IEnumerable<Notification> notifications);
    void Update(Notification notification);
    Task<Notification> GetAsync(int id, CancellationToken cancellationToken = default);
}
