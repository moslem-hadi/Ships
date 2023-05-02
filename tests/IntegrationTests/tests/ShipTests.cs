using Ships.Application.Common.Exceptions;
using Ships.Application.ShipsQR.Commands;
using Ships.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace IntegrationTests;

using static Testing;

public class ShipTests : BaseTestFixture
{
    private CreateShipCommand createCommand;
    public ShipTests()
    {
         createCommand = new()
         {
             Name = "Name",
             Length = 1,
             Width = 1,
             ShipCode = "AAAA-1111-A1"

         };
    }
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateShipCommand();
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldRequireUniqueShipCode()
    {
        await SendAsync(createCommand);
        var anotherCommand = createCommand with { Name = " Ship2" };
        await FluentActions.Invoking(() =>
            SendAsync(anotherCommand)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateCorrectShip()
    {
        var userId = await RunAsUserAsync("administrator@localhost", "Administrator1!" );

        var id = await SendAsync(createCommand);

        var list = await FindAsync<Ship>(id);

        list.Should().NotBeNull();
        list!.Name.Should().Be(createCommand.Name);
        list.CreatedBy.Should().Be(userId);
        list.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }



    [Test]
    public async Task ShouldRequireValidId()
    {
        var invalidId = -1;
        var invalidCommand = new DeleteShipCommand (invalidId);
        await FluentActions.Invoking(() => SendAsync(invalidCommand)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteShip()
    {
        var shipId = await SendAsync(createCommand);

        await SendAsync(new DeleteShipCommand(shipId));

        var list = await FindAsync<Ship>(shipId);

        list.Should().BeNull();
    }
}
