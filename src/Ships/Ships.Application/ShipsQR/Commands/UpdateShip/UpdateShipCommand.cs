using Ships.Application.Common.Exceptions;
using Ships.Application.Common.Interfaces;
using Ships.Domain.Entities;
using MediatR;
using Ships.Application.Common.Security;
using Ships.Application.ShipsQR.Queries;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ships.Domain.ValueObjects;
using System.Xml.Linq;

namespace Ships.Application.ShipsQR.Commands;

[Authorize]
public record UpdateShipCommand : ShipDto, IRequest
{
    public static implicit operator Ship(UpdateShipCommand ship) => new()
    {
        Length = ship.Length,
        Width = ship.Width,
        Name = ship.Name,
        ShipCode = Domain.ValueObjects.ShipCode.From(ship.ShipCode),
        Id = (int)ship.Id,
    };
}

public class UpdateShipCommandHandler : IRequestHandler<UpdateShipCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateShipCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task Handle(UpdateShipCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Ships
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Ship), request.Id);
        }
        //TODO: fix
        entity.Length = request.Length;
        entity.Width = request.Width;
        entity.Name = request.Name;
        entity.ShipCode = ShipCode.From(request.ShipCode);
         
       // _context.Ships.Entry(entity).CurrentValues.SetValues((Ship)(request));
        var x= await _context.SaveChangesAsync(cancellationToken);

    }
}
