namespace FastFoodOrderingSystem.Application.Common.Results;

public class ErrorType
{
    public static readonly ErrorType Validtion = new("Validation");
    public static readonly ErrorType NotFound = new("NotFound");
    public static readonly ErrorType Unathorized = new("Unauthorized");
    public static readonly ErrorType Forbidden = new("Forbidden");
    public static readonly ErrorType Failure = new("Failure");
    public static readonly ErrorType Conflict = new("Conflict");

    public string Value { get; }
    private ErrorType(string value)
    {
        Value = value;
    }
}