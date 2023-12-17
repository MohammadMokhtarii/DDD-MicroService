// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using NotificationManagement.Application.Adapters;
using Services.Common;

namespace NotificationManagement.Infrastructure.Adapters;

[ScopedInjection]
public class SmsAdapter : ISmsAdapter
{
    public Task<string> SendAsync(string receiver, string message)
    {
        throw new NotImplementedException();
    }

    public Task<string> SendBulkAsync(string[] receiver, string message)
    {
        throw new NotImplementedException();
    }
}
