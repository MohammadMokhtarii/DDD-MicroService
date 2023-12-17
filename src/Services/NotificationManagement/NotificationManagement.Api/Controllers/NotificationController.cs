using MediatR;
using Microsoft.AspNetCore.Mvc;
using NotificationManagement.Api.Core;
using NotificationManagement.Application.Commands;

namespace NotificationManagement.Api.Controllers;

public class NotificationController : BaseController
{

    private readonly IMediator _mediator;

    public NotificationController(IMediator mediator) => _mediator = mediator;


    [HttpPost("Queue")]
    public async Task<IActionResult> QueueNotificationsAsync([FromBody] QueueNotificationCommand command, CancellationToken cancellationToken)
        => Ok(await _mediator.Send(command, cancellationToken));



    [HttpPost("Send")]
    public async Task<IActionResult> SendNotificationAsync([FromBody] SendNotificationCommand command, CancellationToken cancellationToken)
        => Ok(await _mediator.Send(command, cancellationToken));
}
