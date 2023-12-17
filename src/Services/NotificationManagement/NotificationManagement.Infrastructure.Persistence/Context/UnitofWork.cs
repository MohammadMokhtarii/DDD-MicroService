// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using NotificationManagement.Domain.Contracts;
using NotificationManagement.Infrastructure.Mediator;
using Services.Common;

namespace NotificationManagement.Infrastructure.Persistence.Context;

[ScopedInjection]
public partial class UnitofWork : IUnitofWork
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMediator _mediator;
    public UnitofWork(ApplicationDbContext dbContext, IMediator mediator)
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
