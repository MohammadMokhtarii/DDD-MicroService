// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace NotificationManagement.Domain.Contracts;
public interface IRepository<T> where T : IAggregateRoot
{
}