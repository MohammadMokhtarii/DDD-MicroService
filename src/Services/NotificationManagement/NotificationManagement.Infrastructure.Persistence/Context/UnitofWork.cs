// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using NotificationManagement.Domain.Contracts;
using NotificationManagement.Infrastructure.Mediator;

namespace NotificationManagement.Infrastructure.Persistence.Context;
public partial class UnitofWork : IUnitofWork
{
    private readonly NotificationManagementContext _dbContext;
    private readonly IMediator _mediator;
    public UnitofWork(NotificationManagementContext dbContext, IMediator mediator)
    {
        _dbContext = dbContext;
        _mediator = mediator;
    }

    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEventsAsync(_dbContext);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }


}
