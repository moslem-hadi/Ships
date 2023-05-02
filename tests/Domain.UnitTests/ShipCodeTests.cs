using Ships.Domain.Exceptions;
using Ships.Domain.ValueObjects;

namespace Domain.UnitTests;

public class Tests
{
    private const string shipCodeMock = "AAAA-1111-A1";
     
    [Test]
    public void FromFunctionShouldReturnCorrectShipCode()
    {
        var code = shipCodeMock;
        var shipCode = ShipCode.From(code);
        shipCode.Code.Should().Be(code);
    }

    [Test]
    public void ToStringReturnsCode()
    {
        var shipCode = ShipCode.From(shipCodeMock);

        shipCode.ToString().Should().Be(shipCode.Code);
        shipCode.ToString().Should().Be(shipCodeMock);
    }

    [Test]
    public void ShouldPerformExplicitConversionGivenAShipCode()
    {
        var shipCode = (ShipCode)shipCodeMock;

        shipCode.Should().Be(shipCodeMock);
    }
    [Test]
    public void ShouldPerformImplicitConversionGivenAShipCode()
    {
        var shipCode = ShipCode.From(shipCodeMock);
        var shipCodeString = (string)shipCode;

        shipCodeString.Should().Be(shipCodeMock);
        shipCodeString.Should().Be(shipCode.Code);
    }

    [Test]
    public void GivenWrongCodeShouldThrowUnsupportedCodeException()
    {
        FluentActions.Invoking(() => ShipCode.From("AAAA-AAAA-AA"))
            .Should().Throw<UnsupportedCodeException>();
    }
}