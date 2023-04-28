using Ships.Application.Common.Mappings;
using Ships.Domain.Entities;

namespace Ships.Application.ShipsQR.Queries.GetShips;

public class ShipDto : IMapFrom<Ship>
{
    public int Id { get; init; }

    public string? Name { get; init; }

    public string? Code { get; init; }

}
