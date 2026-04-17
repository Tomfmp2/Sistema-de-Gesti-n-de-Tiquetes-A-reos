using System;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Domain.ValueObject;

public sealed class DistanceKm
{
    public int? Value { get; }

    private DistanceKm(int? value)
    {
        Value = value;
    }

    public static DistanceKm Create(int? value)
    {
        if (value.HasValue && value.Value <= 0)
            throw new ArgumentException("DistanceKm must be positive if provided", nameof(value));
        return new DistanceKm(value);
    }

    public static DistanceKm Reconstitute(int? value)
    {
        return new DistanceKm(value);
    }
}