using NotificationManagement.Domain.Contracts;

namespace NotificationManagement.Domain.Entities.ReceiverAggregate;

internal interface IReceiverRepository : IRepository<ReceiverInfo>
{
    ReceiverInfo Add(ReceiverInfo receiver);
    void Update(ReceiverInfo receiver);
    ReceiverInfo GetAsync(int id, CancellationToken cancellationToken = default);
}
