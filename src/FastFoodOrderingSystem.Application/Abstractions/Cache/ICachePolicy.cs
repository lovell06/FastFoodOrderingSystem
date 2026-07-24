using FastFoodOrderingSystem.Application.Common.Cqrs;

namespace FastFoodOrderingSystem.Application.Abstractions.Cache;

public interface ICachePolicy<in TRequest>
{
    string GetKey(TRequest request);
    TimeSpan GetTtl();
}