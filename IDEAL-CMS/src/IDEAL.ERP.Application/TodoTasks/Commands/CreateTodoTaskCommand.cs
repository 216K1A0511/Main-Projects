using MediatR;
using IDEAL.ERP.Domain.Entities;
using IDEAL.ERP.Domain.Enums;
using IDEAL.ERP.Application.Common.Interfaces;

namespace IDEAL.ERP.Application.TodoTasks.Commands;

public record CreateTodoTaskCommand(
    string Title,
    string? Description,
    TaskPriority Priority,
    DateTime? ReminderDateTime) : IRequest<Guid>;

public class CreateTodoTaskCommandHandler : IRequestHandler<CreateTodoTaskCommand, Guid>
{
    private readonly IAppDbContext _context;

    public CreateTodoTaskCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateTodoTaskCommand request, CancellationToken cancellationToken)
    {
        var entity = new TodoTask
        {
            Title = request.Title,
            Description = request.Description,
            Priority = request.Priority,
            Status = IDEAL.ERP.Domain.Enums.TaskStatus.Pending,
            ReminderDateTime = request.ReminderDateTime
        };

        _context.TodoTasks.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
