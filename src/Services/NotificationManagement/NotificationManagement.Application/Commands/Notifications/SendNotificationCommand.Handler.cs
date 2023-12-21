using MediatR;
using NotificationManagement.Application.Factories.Notifications.Abstractions;
using NotificationManagement.Domain.Contracts;
using NotificationManagement.Domain.Entities.NotificationAggregate;

namespace NotificationManagement.Application.Commands;

public class SendNotificationCommandHandler(IUnitofWork uow, INotificationFactory notificationFactory) : IRequestHandler<SendNotificationCommand, IActionResponse<string>>
{
    private readonly IUnitofWork _uow = uow;
    private readonly INotificationFactory _notificationFactory = notificationFactory;


    public async Task<IActionResponse<string>> Handle(SendNotificationCommand request, CancellationToken cancellationToken)
    {
        Notification notification = new(request.Receiver, request.Message, request.NotificationTypeId);

        _uow.NotificationRepo.Add(notification);

        var notificationFactory = await _notificationFactory.GetInstance(notification.NotificationType)
                                                            .ExecuteAsync([notification.Receiver], notification.Message, cancellationToken);
        if (notificationFactory.IsSuccess)
            notification.ChangeStatus(NotificationStatus.Successful.Id, notificationFactory.Data);

        var dbResult = await _uow.SaveChangesAsync(cancellationToken);
        if (!dbResult)
            return ActionResponse<string>.Fail(ActionResponseStatusCode.ServerError, "");

        return ActionResponse<string>.Success("");
    }
}
