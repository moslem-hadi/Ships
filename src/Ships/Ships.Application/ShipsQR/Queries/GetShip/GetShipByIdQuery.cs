using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ships.Application.Common.Interfaces;
using Ships.Application.Common.Security;

namespace Ships.Application.ShipsQR.Queries;


[Authorize]
public record GetShipByIdQuery(int ShipId) : IRequest<ShipDto?>
{
}

public class GetShipByIdQueryHandler : IRequestHandler<GetShipByIdQuery, ShipDto?>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetShipByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ShipDto?> Handle(GetShipByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Ships.FirstOrDefaultAsync(a => a.Id == request.ShipId);
        return _mapper.Map<ShipDto>(entity);
            
    }
}
