namespace Ships.Domain.Entities;

public class Ship : BaseAuditableEntity<int>
{
    public string Name { get; set; }
    public int Length { get; set; }
    public int Width { get; set; }
    public ShipCode ShipCode { get; set; }
}