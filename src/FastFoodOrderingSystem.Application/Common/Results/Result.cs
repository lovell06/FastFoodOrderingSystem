namespace FastFoodOrderingSystem.Application.Common.Results;

public sealed class Result<T>
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;

    private readonly T? _value;
    private readonly Error? _error;

    public T Value
        => _value ?? throw new InvalidOperationException("Cannot access Value of a failed result.");

    public Error Error
        => _error ?? throw new InvalidOperationException("Cannot access a Error of a successful result.");

    private Result(T value)
    {
        _value = value;
        IsSuccess = true;
    }

    private Result(Error error)
    {
        ArgumentNullException.ThrowIfNull(error);

        _error = error;
        IsSuccess = false;
    }

    public static Result<T> Success(T value) => new(value);
    public static Result<T> Failure(Error error) => new(error);
}