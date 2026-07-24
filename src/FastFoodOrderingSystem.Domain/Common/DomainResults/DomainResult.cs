namespace FastFoodOrderingSystem.Domain.Common.DomainResults;

public class DomainResult<T>
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;

    private readonly T? _value;

    private readonly DomainError? _error;

    public T Value => 
        _value ?? throw new InvalidOperationException("Cannot access Value of a failed result.");

    public DomainError Error =>
        _error ?? throw new InvalidOperationException("Cannot access Error of a successful result.");

    private DomainResult(bool isSuccess = true)
    {
        IsSuccess = isSuccess;
    }

    private DomainResult(T value)
    {
        _value = value;
        IsSuccess = true;
    }

    private DomainResult(DomainError error)
    {
        _error = error;
        IsSuccess = false;
    }

    public static DomainResult<T> Success()
    {
        return new DomainResult<T>();
    }
    public static DomainResult<T> Success(T value)
    {
        return new DomainResult<T>(value);
    }

    public static DomainResult<T> Failure()
    {
        return new DomainResult<T>(false);
    }

    public static DomainResult<T> Failure(DomainError error)
    {
        return new DomainResult<T>(error);
    }
}