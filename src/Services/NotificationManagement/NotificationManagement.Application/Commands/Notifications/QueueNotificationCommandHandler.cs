using MediatR;
using NotificationManagement.Domain.Contracts;
using NotificationManagement.Domain.Entities.NotificationAggregate;

namespace NotificationManagement.Application.Commands;

public class QueueNotificationCommandHandler : IRequestHandler<QueueNotificationCommand, bool>
{
    private readonly IUnitofWork _uow;
    public QueueNotificationCommandHandler(IUnitofWork uow) => _uow = uow;

    public async Task<bool> Handle(QueueNotificationCommand request, CancellationToken cancellationToken)
    {
        IEnumerable<Notification> notifications = request.Receivers.Select(row => new Notification(row, request.Message, request.NotificationTypeId)).ToList();

        _uow.NotificationRepo.AddRange(notifications);

        return await _uow.SaveChangesAsync(cancellationToken);
    }
}
