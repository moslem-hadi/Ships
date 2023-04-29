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
    public string Sort { get; set; } 
    public string Filter { get; set; } 
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
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

        var ships = _context.Ships.AsQueryable();
        if (!String.IsNullOrEmpty(request.Filter))
        {
            ships = ships.Where(s => s.Name.Contains(request.Filter));
        }
        switch (request.Sort)
        {
            case "name_desc":
                ships = ships.OrderByDescending(s => s.Name);
                break;
            case "width_desc":
                ships = ships.OrderByDescending(s => s.Width);
                break;
            case "length_desc":
                ships = ships.OrderByDescending(s => s.Length);
                break;
            case "width":
                ships = ships.OrderBy(s => s.Width);
                break;
            case "length":
                ships = ships.OrderBy(s => s.Length);
                break;
            default:
                ships = ships.OrderBy(s => s.Name);
                break;
        }



        return await ships
            .ProjectTo<ShipDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.Page, request.PageSize);
    }
}
