using Ships.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ships.Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;
using Ships.Infrastructure.Identity;

namespace Ships.Infrastructure.Persistence;

public class ApplicationDbContextInitializer
{
    private readonly ILogger<ApplicationDbContextInitializer> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ApplicationDbContextInitializer(ILogger<ApplicationDbContextInitializer> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitializeAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initializing the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default roles
        var administratorRole = new IdentityRole("Administrator");

        if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
        {
            await _roleManager.CreateAsync(administratorRole);
        }

        // Default users
        var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

        if (_userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await _userManager.CreateAsync(administrator, "Administrator1!");
            if (!string.IsNullOrWhiteSpace(administratorRole.Name))
            {
                await _userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
            }
        }
        // Default data
        if (!_context.Ships.Any())
        {
            _context.Ships.AddRange(new List<Ship>()
            {
                new Ship
            {
                Name = "Ship Number 1",
                ShipCode = ShipCode.From("AAAA-1111-A1"),
                Length = 40,
                Width = 11
            },  new Ship
            {
                Name = "Ship Number 2",
                ShipCode = ShipCode.From("AAAA-1111-A2"),
                Length = 39,
                Width = 23
            },  new Ship
            {
                Name = "Ship Number 3",
                ShipCode = ShipCode.From("AAAA-1111-A3"),
                Length = 74,
                Width = 45
            },  new Ship
            {
                Name = "Ship Number 4",
                ShipCode = ShipCode.From("AAAA-1111-A4"),
                Length = 36,
                Width = 34
            },  new Ship
            {
                Name = "Ship Number 5",
                ShipCode = ShipCode.From("AAAA-1111-A5"),
                Length = 45,
                Width = 54
            },  new Ship
            {
                Name = "Ship Number 6",
                ShipCode = ShipCode.From("AAAA-1111-A6"),
                Length = 67,
                Width = 13
            },  new Ship
            {
                Name = "Ship Number 7",
                ShipCode = ShipCode.From("AAAA-1111-A7"),
                Length = 21,
                Width = 10
            },  new Ship
            {
                Name = "Ship Number 8",
                ShipCode = ShipCode.From("AAAA-1111-A8"),
                Length = 43,
                Width = 10
            },  new Ship
            {
                Name = "Ship Number 9",
                ShipCode = ShipCode.From("AAAA-1111-A9"),
                Length = 25,
                Width = 75
            },  new Ship
            {
                Name = "Ship Number 10",
                ShipCode = ShipCode.From("AAAA-1111-B1"),
                Length = 45,
                Width = 47
            },
            });

            await _context.SaveChangesAsync();
        }
    }
}
