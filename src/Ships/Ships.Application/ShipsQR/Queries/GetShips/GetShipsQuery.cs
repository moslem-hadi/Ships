using AutoMapper;
using AutoMapper.QueryableExtensions;
using Ships.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ships.Application.ShipsQR.Queries;
using System.Collections.ObjectModel;
using Ships.Application.Common.Mappings;
using Ships.Application.Common.Models;

namespace Ships.Application.ShipsQR.Queries.GetShips;

public class GetShipsQuery : IRequest<PaginatedList<ShipDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetShipsQueryHandler : IRequestHandler<GetShipsQuery, PaginatedList<ShipDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetShipsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ShipDto>> Handle(GetShipsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Ships
            .OrderBy(x => x.Name)
            .ProjectTo<ShipDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
