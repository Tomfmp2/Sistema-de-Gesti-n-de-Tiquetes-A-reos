using System;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Domain.ValueObject;

public sealed class EstimatedDurationMin
{
    public int? Value { get; }

    private EstimatedDurationMin(int? value)
    {
        Value = value;
    }

    public static EstimatedDurationMin Create(int? value)
    {
        if (value.HasValue && value.Value <= 0)
            throw new ArgumentException("EstimatedDurationMin must be positive if provided", nameof(value));
        return new EstimatedDurationMin(value);
    }

    public static EstimatedDurationMin Reconstitute(int? value)
    {
        return new EstimatedDurationMin(value);
    }
}