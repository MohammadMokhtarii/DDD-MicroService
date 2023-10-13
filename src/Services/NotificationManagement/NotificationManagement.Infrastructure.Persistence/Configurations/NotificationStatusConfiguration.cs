using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotificationManagement.Domain.Entities.NotificationAggregate;
using NotificationManagement.Infrastructure.Persistence.Context;

namespace NotificationManagement.Infrastructure.Persistence.Configurations;

internal class NotificationStatusConfiguration : IEntityTypeConfiguration<NotificationStatus>
{
    public void Configure(EntityTypeBuilder<NotificationStatus> configuration)
    {
        configuration.ToTable("NotificationStatuses", NotificationManagementContext.DEFAULT_SCHEMA);

        configuration.HasKey(e => e.Id);

        configuration.Property(ct => ct.Id)
                     .HasDefaultValue(1)
                     .ValueGeneratedNever()
                     .IsRequired();

        configuration.Property(ct => ct.Name)
                     .HasMaxLength(200)
                     .IsRequired();
    }
}
