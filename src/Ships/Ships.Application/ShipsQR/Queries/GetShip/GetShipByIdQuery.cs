using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ships.Application.Common.Interfaces;
using Ships.Application.Common.Models;
using Ships.Application.Common.Security;
using Ships.Application.ShipsQR.Queries.GetShips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ships.Application.ShipsQR.Queries.GetShip;


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
