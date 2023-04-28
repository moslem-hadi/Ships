using Ships.Application.Common.Exceptions;
using Ships.Application.Common.Interfaces;
using Ships.Domain.Entities;
using MediatR;
using Ships.Domain.ValueObjects;

namespace Ships.Application.ShipsQR.Commands.UpdateTodoList;

public record UpdateShipCommand : IRequest
{
    public int Id { get; init; }
    public string Name { get; init; }
    public int Length { get; set; }
    public int Width { get; set; }
    public string Code { get; set; }
}

public class UpdateTodoListCommandHandler : IRequestHandler<UpdateShipCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateTodoListCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateShipCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Ships
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Ship), request.Id);
        }

        entity.Name = request.Name;
        entity.Length = request.Length;
        entity.Width = request.Width;
        entity.Code = ShipCode.From(request.Code);

        await _context.SaveChangesAsync(cancellationToken);

    }
}
