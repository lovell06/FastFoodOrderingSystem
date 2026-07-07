namespace FastFoodOrderingSystem.Domain.Common.DomainResults;

public class DomainResult<T>
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;

    public T? Value { get; }
    public DomainError? Error { get; }

    private DomainResult(bool isSuccess = true)
    {
        IsSuccess = isSuccess;
    }

    private DomainResult(T value)
    {
        Value = value;
        IsSuccess = true;
    }

    private DomainResult(DomainError error)
    {
        Error = error;
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