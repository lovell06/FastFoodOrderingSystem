using System.Diagnostics.CodeAnalysis;

namespace FastFoodOrderingSystem.Application.Common.Cqrs;

public struct Unit
{
    public static readonly Unit Value = new();

    public bool Equals(Unit other) => true;
    public override bool Equals([NotNullWhen(true)] object? obj) => obj is Unit;
    public override int GetHashCode() => 0;
    public override string ToString() => "()";
}