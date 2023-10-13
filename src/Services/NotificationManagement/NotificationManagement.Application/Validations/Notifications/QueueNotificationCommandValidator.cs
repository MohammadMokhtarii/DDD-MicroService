using FluentValidation;
using NotificationManagement.Application.Commands;
using NotificationManagement.Domain.Contracts;
using NotificationManagement.Domain.Entities.NotificationAggregate;

namespace NotificationManagement.Application.Validations.Notifications;

internal class QueueNotificationCommandValidator : AbstractValidator<QueueNotificationCommand>
{
    public QueueNotificationCommandValidator()
    {
        RuleFor(x => x.Receivers).NotEmpty().WithMessage("Receiver cant be empty").ChildRules(x =>
        {
            RuleFor(x => x.Message).NotEmpty().WithMessage("Receiver cant be empty")
                                   .MaximumLength(50).WithMessage("Message cant be more than 50 character");
        });
                                

        RuleFor(x => x.Message).NotEmpty().WithMessage("Message cant be empty")
                                .MaximumLength(500).WithMessage("Message cant be more than 500 character");

        var validNotificationTypeIds = Enumeration.GetAll<NotificationType>().Select(x => x.Id).ToArray();
        RuleFor(x => x.NotificationTypeId).In(validNotificationTypeIds);
    }
}
