// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore.Infrastructure;
using NotificationManagement.Domain.Entities.NotificationAggregate;

namespace NotificationManagement.Infrastructure.Persistence.Context;
public partial class UnitofWork
{

    public INotificationRepository NotificationRepo => _dbContext.GetService<INotificationRepository>();
}
