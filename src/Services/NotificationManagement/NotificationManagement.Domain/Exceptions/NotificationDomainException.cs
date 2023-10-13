// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace NotificationManagement.Domain.Exceptions;
public class NotificationDomainException : Exception
{
    public NotificationDomainException()
    { }

    public NotificationDomainException(string message)
        : base(message)
    { }

    public NotificationDomainException(string message, Exception innerException)
        : base(message, innerException)
    { }
}
