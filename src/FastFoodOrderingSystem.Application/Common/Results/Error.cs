namespace FastFoodOrderingSystem.Application.Common.Results;

public class Error
{
    public string Code { get; init; }
    public string Message { get; init; }
    public ErrorType Type { get; init; }

    private Error(string code, string message, ErrorType type)
    {
        Code = code;
        Message = message;
        Type = type;
    }

    public static Error Validation(string code, string message)
    {
        return new(code, message, ErrorType.Validtion);
    }

    public static Error NotFound(string code, string message)
    {
        return new(code, message, ErrorType.NotFound);
    }

    public static Error Unauthorized(string code, string message)
    {
        return new(code, message, ErrorType.Unathorized);
    }

    public static Error Forbidden(string code, string message)
    {
        return new(code, message, ErrorType.Forbidden);
    }

    public static Error Failure(string code, string message)
    {
        return new(code, message, ErrorType.Failure);
    }

    public static Error Conflict(string code, string message)
    {
        return new(code, message, ErrorType.Conflict);
    }

    public static Error Business(string code, string message)
    {
        return new(code, message, ErrorType.Business);
    }
}