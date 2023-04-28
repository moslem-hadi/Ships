using Ships.Application.Common.Exceptions;
using Ships.Application.Common.Interfaces;
using Ships.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ships.Application.ShipsQR.Commands.DeleteTodoList;

public record DeleteShipCommand(int Id) : IRequest;

public class DeleteTodoListCommandHandler : IRequestHandler<DeleteShipCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteTodoListCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteShipCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Ships
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Ship), request.Id);
        }

        _context.Ships.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
