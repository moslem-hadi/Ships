using Ships.Application.Common.Interfaces;

namespace Ships.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
