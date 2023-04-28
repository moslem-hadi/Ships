using Ships.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ships.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Ship> Ships { get; }


    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
