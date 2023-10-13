// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using NotificationManagement.Domain.Entities.NotificationAggregate;
using NotificationManagement.Infrastructure.Persistence.Context;

namespace NotificationManagement.Infrastructure.Persistence.Repositories;
public class NotificationRepository : INotificationRepository
{
    private readonly NotificationManagementContext _context;
    public NotificationRepository(NotificationManagementContext context)
    {
        ArgumentNullException.ThrowIfNull(nameof(context));
        _context = context;
    }

    public Notification Add(Notification notification)
        => _context.Notifications.Add(notification).Entity;

    public void Update(Notification notification)
        => _context.Entry(notification).State = EntityState.Modified;

    public async Task<Notification> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        var notification = await _context.Notifications.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (notification is not null)
        {
            await _context.Entry(notification).Collection(i => i.NotificationAcitvities).LoadAsync(cancellationToken);
            await _context.Entry(notification).Reference(i => i.NotificationStatus).LoadAsync(cancellationToken);
            await _context.Entry(notification).Reference(i => i.NotificationPriority).LoadAsync(cancellationToken);
            await _context.Entry(notification).Reference(i => i.NotificationType).LoadAsync(cancellationToken);
        }

        return notification;
    }

}
