using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotificationManagement.Domain.Entities.NotificationAggregate;
using NotificationManagement.Infrastructure.Persistence.Context;

namespace NotificationManagement.Infrastructure.Persistence.Configurations;

internal class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> configuration)
    {
        configuration.ToTable("Notifications", NotificationManagementContext.DEFAULT_SCHEMA);

        configuration.HasKey(e => e.Id);

        configuration.Ignore(e => e.DomainEvents);

        configuration.Property(e => e.Id)
                     .UseHiLo("notificationseq", NotificationManagementContext.DEFAULT_SCHEMA);

        configuration.Property(e => e.Receiver)
                     .HasMaxLength(50)
                     .IsRequired();

        configuration.Property(e => e.Message)
                     .HasMaxLength(500)
                     .IsRequired();

        configuration.Property<int>("_retryCount")
                     .UsePropertyAccessMode(PropertyAccessMode.Field)
                     .HasColumnName("RetryCount")
                     .IsRequired();

        configuration.Property<bool>("_read")
                     .UsePropertyAccessMode(PropertyAccessMode.Field)
                     .HasColumnName("Read")
                     .IsRequired();

        configuration.Property<int>("_notificationTypeId")
                     .UsePropertyAccessMode(PropertyAccessMode.Field)
                     .HasColumnName("NotificationTypeId")
                     .IsRequired();

        configuration.Property<int>("_notificationPriorityId")
                     .UsePropertyAccessMode(PropertyAccessMode.Field)
                     .HasColumnName("NotificationPriorityId")
                     .IsRequired();

        configuration.Property<int>("_notificationStatusId")
                     .UsePropertyAccessMode(PropertyAccessMode.Field)
                     .HasColumnName("NotificationStatusId")
                     .IsRequired();

        configuration.HasMany(e => e.NotificationAcitvities)
                     .WithOne()
                     .HasForeignKey("NotificationId");

        var navigation = configuration.Metadata.FindNavigation(nameof(Notification.NotificationAcitvities));
        navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
