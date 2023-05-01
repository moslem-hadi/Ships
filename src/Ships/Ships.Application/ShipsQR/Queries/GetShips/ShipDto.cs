using Ships.Application.Common.Mappings;
using Ships.Domain.Entities;

namespace Ships.Application.ShipsQR.Queries;

public  class ShipDto : IMapFrom<Ship>
{
    public int? Id { get; set; }

    public string? Name { get; set; }
    
    public int Length { get; set; }

    public int Width { get; set; }

    public string ShipCode { get; set; }

}
