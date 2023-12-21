using NotificationManagement.Domain.Entities.NotificationAggregate;

namespace NotificationManagement.Application.Factories.Notifications.Abstractions;

public interface INotificationFactory
{
    INotificationStrategy GetInstance(NotificationType type);
}
