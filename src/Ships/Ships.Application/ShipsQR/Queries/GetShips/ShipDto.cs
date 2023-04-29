using Ships.Application.Common.Mappings;
using Ships.Domain.Entities;
using Ships.Domain.ValueObjects;

namespace Ships.Application.ShipsQR.Queries.GetShips;

public class ShipDto : IMapFrom<Ship>
{
    public int Id { get; init; }

    public string? Name { get; init; }
    
    public int Length { get; set; }

    public int Width { get; set; }

    public ShipCode? ShipCode { get; init; }

}
