namespace Ships.Domain.Events;

public class ShipCreatedEvent : BaseEvent
{
    public ShipCreatedEvent(Ship ship)
    {
        Ship = ship;
    }

    public Ship Ship { get; }
}
