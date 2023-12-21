// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.Extensions.DependencyInjection;
using NotificationManagement.Application.Factories.Notifications.Abstractions;
using NotificationManagement.Domain.Entities.NotificationAggregate;

namespace NotificationManagement.Application.Factories.Notifications;

[ScopedInjection]
public class NotificationFactory(IServiceProvider serviceProvider) : INotificationFactory
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public INotificationStrategy GetInstance(NotificationType type) => _serviceProvider.GetRequiredKeyedService<INotificationStrategy>(type.Name);
}
