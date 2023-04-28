using Ships.Application.Common.Interfaces;
using Ships.Domain.Entities;
using MediatR;
using Ships.Domain.ValueObjects;

namespace Ships.Application.ShipsQR.Commands.CreateTodoList;

public record CreateShipCommand : IRequest<int>
{
    public string Name { get; init; }
    public int Length { get; set; }
    public int Width { get; set; }
    public string Code { get; set; }
}

public class CreateTodoListCommandHandler : IRequestHandler<CreateShipCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateTodoListCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateShipCommand request, CancellationToken cancellationToken)
    {
        var entity = new Ship();

        entity.Name = request.Name;
        entity.Length = request.Length;
        entity.Width = request.Width;
        entity.Code = ShipCode.From(request.Code);

        _context.Ships.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
