using FastFoodOrderingSystem.Application.Abstractions.Time;

namespace FastFoodOrderingSystem.Infrastructure.Time;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}