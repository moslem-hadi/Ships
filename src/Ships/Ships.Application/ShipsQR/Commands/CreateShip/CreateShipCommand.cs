using Ships.Application.Common.Interfaces;
using Ships.Domain.Entities;
using MediatR;
using Ships.Domain.ValueObjects;
using Ships.Application.Common.Security;
using Ships.Application.ShipsQR.Queries;
using AutoMapper;
using Ships.Domain.Events;

namespace Ships.Application.ShipsQR.Commands;

[Authorize]
public record CreateShipCommand : ShipDto, IRequest<int>
{
    public static implicit operator Ship(CreateShipCommand ship) => new()
    {
        Length = ship.Length,
        Width = ship.Width,
        Name = ship.Name,
        ShipCode = Domain.ValueObjects.ShipCode.From(ship.ShipCode)
    };
}

public class CreateShipCommandHandler : IRequestHandler<CreateShipCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateShipCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateShipCommand request, CancellationToken cancellationToken)
    {
        var entity = (Ship)request;
        //var entity = _mapper.Map<Ship>(request)

        entity.AddDomainEvent(new ShipCreatedEvent(entity));
        _context.Ships.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
