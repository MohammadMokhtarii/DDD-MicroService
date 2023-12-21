// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using NotificationManagement.Domain.Entities.NotificationAggregate;
using NotificationManagement.Application.Adapters;
using NotificationManagement.Application.Factories.Notifications.Abstractions;

namespace NotificationManagement.Application.Factories.Notifications;

[ScopedInjection(nameof(NotificationType.Sms))]
public class SmsStrategy(ISmsAdapter smsAdaptar) : INotificationStrategy
{
    private readonly ISmsAdapter _smsAdaptar = smsAdaptar;

    public async Task<IActionResponse<string>> ExecuteAsync(string[] receivers, object message, CancellationToken cancellationToken)
    {
        if (!receivers.Any())
            return new ActionResponse<string>(ActionResponseStatusCode.BadRequest, "");

        IActionResponse<string> result;
        if (receivers.Length > 1)
            result = await _smsAdaptar.SendBulkAsync(receivers, message.ToString());
        else
            result = await _smsAdaptar.SendAsync(receivers[0], message.ToString());

        if (!result.IsSuccess)
            return ActionResponse<string>.Fail(result.StatusCode, result.Message);

        return ActionResponse<string>.Success(result.Data);
    }
}
