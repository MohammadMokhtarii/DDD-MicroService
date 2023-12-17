using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotificationManagement.Domain.Entities.NotificationAggregate;
using NotificationManagement.Infrastructure.Persistence.Context;

namespace NotificationManagement.Infrastructure.Persistence.Configurations;

internal class NotificationAcitvityConfiguration : IEntityTypeConfiguration<NotificationAcitvity>
{
    public void Configure(EntityTypeBuilder<NotificationAcitvity> configuration)
    {
        configuration.ToTable("NotificationAcitvities", ApplicationDbContext.DEFAULT_SCHEMA);

        configuration.HasKey(e => e.Id);

        configuration.Ignore(e => e.DomainEvents);

        configuration.Property(e => e.Id)
                     .ValueGeneratedOnAdd();

        configuration.Property<string>("_response")
                     .UsePropertyAccessMode(PropertyAccessMode.Field)
                     .HasColumnName("Response")
                     .HasMaxLength(500)
                     .IsRequired();

        configuration.Property<DateTime>("_createdOn")
                     .UsePropertyAccessMode(PropertyAccessMode.Field)
                     .HasColumnName("CreatedOn")
                     .IsRequired();

        configuration.Property<int>("_notificationId")
                     .UsePropertyAccessMode(PropertyAccessMode.Field)
                     .HasColumnName("NotificationId")
                     .IsRequired();

        configuration.Property<int>("_notificationStatusId")
                     .UsePropertyAccessMode(PropertyAccessMode.Field)
                     .HasColumnName("NotificationStatusId")
                     .IsRequired();

    }
}
