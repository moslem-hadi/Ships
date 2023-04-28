namespace Ships.Domain.Events;

public class ShipDeletedEvent : BaseEvent
{
    public ShipDeletedEvent(Ship ship)
    {
        Ship = ship;
    }

    public Ship Ship { get; }
}
