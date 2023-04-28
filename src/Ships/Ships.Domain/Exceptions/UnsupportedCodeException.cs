namespace Ships.Domain.Exceptions;

public class UnsupportedCodeException : Exception
{
    public UnsupportedCodeException(string code)
        : base($"Code \"{code}\" is unsupported.")
    {
    }
}
