using MediatR;
using NotificationManagement.Domain.Contracts;
using NotificationManagement.Domain.Entities.NotificationAggregate;

namespace NotificationManagement.Application.Commands;

public class QueueNotificationCommandHandler(IUnitofWork uow) : IRequestHandler<QueueNotificationCommand, IActionResponse>
{
    private readonly IUnitofWork _uow = uow;

    public async Task<IActionResponse> Handle(QueueNotificationCommand request, CancellationToken cancellationToken)
    {
        IEnumerable<Notification> notifications = request.Receivers.Select(row => new Notification(row, request.Message, request.NotificationTypeId)).ToList();

        _uow.NotificationRepo.AddRange(notifications);

        var dbResult = await _uow.SaveChangesAsync(cancellationToken);
        if (!dbResult)
            return ActionResponse.Fail(ActionResponseStatusCode.ServerError, "");

        return ActionResponse.Success();
    }
}
