// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NotificationManagement.Domain.Entities.NotificationAggregate;
using NotificationManagement.Infrastructure.Persistence.Configurations;

namespace NotificationManagement.Infrastructure.Persistence.Context;
public class NotificationManagementContext : DbContext
{
    public const string DEFAULT_SCHEMA = "NotificationManagement";

    public DbSet<Notification> Notifications { get; set; }
    public DbSet<NotificationAcitvity> NotificationAcitvities { get; set; }
    public DbSet<NotificationPriority> NotificationPriorities { get; set; }
    public DbSet<NotificationStatus> NotificationStatuses { get; set; }
    public DbSet<NotificationType> NotificationTypes { get; set; }


    private IDbContextTransaction _currentTransaction;
    public bool HasActiveTransaction => _currentTransaction != null;

    public NotificationManagementContext(DbContextOptions<NotificationManagementContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new NotificationConfiguration());
        modelBuilder.ApplyConfiguration(new NotificationAcitvityConfiguration());
        modelBuilder.ApplyConfiguration(new NotificationPriorityConfiguration());
        modelBuilder.ApplyConfiguration(new NotificationStatusConfiguration());
        modelBuilder.ApplyConfiguration(new NotificationTypeConfiguration());
    }


    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        if (_currentTransaction != null) return null;

        _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

        return _currentTransaction;
    }

    public async Task CommitTransactionAsync(IDbContextTransaction transaction)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

        try
        {
            await SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            RollbackTransaction();
            throw;
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

    public void RollbackTransaction()
    {
        try
        {
            _currentTransaction?.Rollback();
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }
}
