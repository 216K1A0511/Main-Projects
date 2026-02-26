using MediatR;
using IDEAL.ERP.Application.Common.Interfaces;

namespace IDEAL.ERP.Application.TodoTasks.Commands;

public record DeleteTodoTaskCommand(Guid Id) : IRequest;

public class DeleteTodoTaskCommandHandler : IRequestHandler<DeleteTodoTaskCommand>
{
    private readonly IAppDbContext _context;

    public DeleteTodoTaskCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteTodoTaskCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.TodoTasks.FindAsync(request.Id);
        if (entity != null)
        {
            _context.TodoTasks.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
