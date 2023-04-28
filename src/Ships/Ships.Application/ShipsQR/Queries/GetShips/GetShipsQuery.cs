using AutoMapper;
using AutoMapper.QueryableExtensions;
using Ships.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ships.Application.ShipsQR.Queries;
using System.Collections.ObjectModel;

namespace Ships.Application.ShipsQR.Queries.GetShips;

public record GetShipsQuery : IRequest<ReadOnlyCollection<ShipDto>>;

public class GetShipsQueryHandler : IRequestHandler<GetShipsQuery, ReadOnlyCollection<ShipDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetShipsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ReadOnlyCollection<ShipDto>> Handle(GetShipsQuery request, CancellationToken cancellationToken)
    {
        //return new ShipVm
        //{
        //    PriorityLevels = Enum.GetValues(typeof(PriorityLevel))
        //        .Cast<PriorityLevel>()
        //        .Select(p => new PriorityLevelDto { Value = (int)p, Name = p.ToString() })
        //        .ToList(),

        //    Lists = await _context.TodoLists
        //        .AsNoTracking()
        //        .ProjectTo<TodoListDto>(_mapper.ConfigurationProvider)
        //        .OrderBy(t => t.Title)
        //        .ToListAsync(cancellationToken)
        //};
        return null;
    }
}
