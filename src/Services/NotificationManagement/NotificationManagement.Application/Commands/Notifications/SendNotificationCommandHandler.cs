using MediatR;
using NotificationManagement.Application.Adapters;
using NotificationManagement.Domain.Contracts;
using NotificationManagement.Domain.Entities.NotificationAggregate;

namespace NotificationManagement.Application.Commands.Notifications;

public class SendNotificationCommandHandler : IRequestHandler<SendNotificationCommand, bool>
{
    private readonly IUnitofWork _uow;
    private readonly ISmsAdapter _smsAdapter;
    public SendNotificationCommandHandler(IUnitofWork uow, ISmsAdapter smsAdapter)
    {
        _uow = uow;
        _smsAdapter = smsAdapter;
    }

    public async Task<bool> Handle(SendNotificationCommand request, CancellationToken cancellationToken)
    {
        Notification notification = new(request.Receiver, request.Message, request.NotificationTypeId);

        _uow.NotificationRepo.Add(notification);

        var smsResult = await _smsAdapter.SendAsync(notification.Receiver, notification.Message);
        if (!string.IsNullOrEmpty(smsResult))
            notification.ChangeStatus(NotificationStatus.Successful.Id, smsResult);

        return await _uow.SaveChangesAsync(cancellationToken);
    }
}
