using Ships.Application.Common.Exceptions;
using Ships.Application.Common.Interfaces;
using Ships.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ships.Application.Common.Security;
using Ships.Domain.Events;

namespace Ships.Application.ShipsQR.Commands;

[Authorize]
public record DeleteShipCommand(int Id) : IRequest;

public class DeleteShipCommandHandler : IRequestHandler<DeleteShipCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteShipCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteShipCommand request, CancellationToken cancellationToken)
    {

        var entity = await _context.Ships
          .FindAsync(new object[] { request.Id }, cancellationToken) 
          ?? throw new NotFoundException(nameof(Ships), request.Id);

        entity.AddDomainEvent(new ShipDeletedEvent(entity));

        _context.Ships.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
