using Ships.Application.Common.Interfaces;
using Ships.Domain.Entities;
using MediatR;
using Ships.Domain.ValueObjects;
using Ships.Application.Common.Security;
using Ships.Application.ShipsQR.Queries;
using AutoMapper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Ships.Application.ShipsQR.Commands;

[Authorize]
public class CreateShipCommand : ShipDto, IRequest<int>
{
    public int csddcsc { get; set; }
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
    private readonly IMapper _mapper;

    public CreateShipCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateShipCommand request, CancellationToken cancellationToken)
    {
        var entity = (Ship)request;

        _context.Ships.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
