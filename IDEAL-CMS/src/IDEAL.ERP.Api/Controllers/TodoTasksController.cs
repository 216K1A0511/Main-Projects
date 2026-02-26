using MediatR;
using Microsoft.AspNetCore.Mvc;
using IDEAL.ERP.Application.TodoTasks.Commands;
using IDEAL.ERP.Domain.Enums;

namespace IDEAL.ERP.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoTasksController : ControllerBase
{
    private readonly IMediator _mediator;

    public TodoTasksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTodoTaskCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteTodoTaskCommand(id));
        return NoContent();
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] IDEAL.ERP.Domain.Enums.TaskStatus status)
    {
        await _mediator.Send(new UpdateTodoTaskStatusCommand(id, status));
        return NoContent();
    }
}
