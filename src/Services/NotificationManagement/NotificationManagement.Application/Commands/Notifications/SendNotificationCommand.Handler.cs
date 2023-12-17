using MediatR;
using NotificationManagement.Application.Adapters;
using NotificationManagement.Domain.Contracts;
using NotificationManagement.Domain.Entities.NotificationAggregate;
using Services.Common;

namespace NotificationManagement.Application.Commands;

public class SendNotificationCommandHandler : IRequestHandler<SendNotificationCommand, IActionResponse<string>>
{
    private readonly IUnitofWork _uow;
    private readonly ISmsAdapter _smsAdapter;
    public SendNotificationCommandHandler(IUnitofWork uow, ISmsAdapter smsAdapter)
    {
        _uow = uow;
        _smsAdapter = smsAdapter;
    }

    public async Task<IActionResponse<string>> Handle(SendNotificationCommand request, CancellationToken cancellationToken)
    {
        Notification notification = new(request.Receiver, request.Message, request.NotificationTypeId);

        _uow.NotificationRepo.Add(notification);

        var smsResult = await _smsAdapter.SendAsync(notification.Receiver, notification.Message);
        if (!string.IsNullOrEmpty(smsResult))
            notification.ChangeStatus(NotificationStatus.Successful.Id, smsResult);

        var dbResult = await _uow.SaveChangesAsync(cancellationToken);
        if (!dbResult)
            return ActionResponse<string>.Fail(ActionResponseStatusCode.ServerError, "");

        return ActionResponse<string>.Success("");
    }
}
