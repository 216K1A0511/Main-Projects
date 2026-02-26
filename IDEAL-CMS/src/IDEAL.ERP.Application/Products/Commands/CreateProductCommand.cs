using MediatR;
using IDEAL.ERP.Domain.Entities;
using IDEAL.ERP.Application.Common.Interfaces;

namespace IDEAL.ERP.Application.Products.Commands;

public record CreateProductCommand(
    string Name,
    string Sku,
    decimal Price,
    int StockQuantity,
    string Category) : IRequest<Guid>;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IAppDbContext _context;

    public CreateProductCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = new Product
        {
            Name = request.Name,
            Sku = request.Sku,
            Price = request.Price,
            StockQuantity = request.StockQuantity,
            Category = request.Category
        };

        _context.Products.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
