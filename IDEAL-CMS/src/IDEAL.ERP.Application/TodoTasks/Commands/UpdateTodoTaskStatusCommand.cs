using MediatR;
using IDEAL.ERP.Domain.Enums;
using IDEAL.ERP.Application.Common.Interfaces;

namespace IDEAL.ERP.Application.TodoTasks.Commands;

public record UpdateTodoTaskStatusCommand(Guid Id, IDEAL.ERP.Domain.Enums.TaskStatus Status) : IRequest;

public class UpdateTodoTaskStatusCommandHandler : IRequestHandler<UpdateTodoTaskStatusCommand>
{
    private readonly IAppDbContext _context;

    public UpdateTodoTaskStatusCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateTodoTaskStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.TodoTasks.FindAsync(request.Id);
        if (entity != null)
        {
            entity.Status = request.Status;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
