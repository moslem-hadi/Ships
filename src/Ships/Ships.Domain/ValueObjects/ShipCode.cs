using System.Text.RegularExpressions;

namespace Ships.Domain.ValueObjects;

public sealed class ShipCode : ValueObject
{
    static ShipCode()
    {
    }

    private ShipCode()
    {
    }

    private ShipCode(string code)
    {
        Code = code;
    }

    public static ShipCode From(string code)
    {
        var shipCode = new ShipCode { Code = code };

        if (!Validate(shipCode))
        {
            throw new UnsupportedCodeException(code);
        }

        return shipCode;
    }

    public string Code { get; private set; } = "AAAA-1111-A1";

    private static bool Validate(ShipCode shipCode)
    {
        if (shipCode.Code.Length != 12)
            return false;
        var regex = @"^[a-zA-Z]{4}[-]{1}\d{4}[-]{1}[a-zA-Z]{1}\d{1}$";
        var match = Regex.Match(shipCode.Code, regex, RegexOptions.IgnoreCase);
        return match.Success;
    }

    public static implicit operator string(ShipCode code)
    {
        return code.ToString();
    }

    public static explicit operator ShipCode(string code)
    {
        return From(code);
    }

    public override string ToString()
    {
        return Code;
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Code;
    }
}
