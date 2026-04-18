namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.BaggageTypes.Domain.Aggregate;

using ValueObjects;

public sealed class BaggageType
{
    public BaggageTypeId Id { get; private set; }
    public BaggageTypeName Name { get; private set; }
    public MaxWeightKg MaxWeightKg { get; private set; }
    public BasePrice BasePrice { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private BaggageType()
    {
    }

    public static BaggageType Create(
        BaggageTypeName name,
        MaxWeightKg maxWeightKg,
        BasePrice basePrice)
    {
        return new BaggageType
        {
            Id = null!, // Will be set by database
            Name = name,
            MaxWeightKg = maxWeightKg,
            BasePrice = basePrice,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
    }

    public static BaggageType Reconstitute(
        BaggageTypeId id,
        BaggageTypeName name,
        MaxWeightKg maxWeightKg,
        BasePrice basePrice,
        DateTime createdAt,
        DateTime updatedAt)
    {
        return new BaggageType
        {
            Id = id,
            Name = name,
            MaxWeightKg = maxWeightKg,
            BasePrice = basePrice,
            CreatedAt = createdAt,
            UpdatedAt = updatedAt
        };
    }

    public void UpdateName(BaggageTypeName newName)
    {
        Name = newName;
        UpdatedAt = DateTime.Now;
    }

    public void UpdateMaxWeightKg(MaxWeightKg newMaxWeightKg)
    {
        MaxWeightKg = newMaxWeightKg;
        UpdatedAt = DateTime.Now;
    }

    public void UpdateBasePrice(BasePrice newBasePrice)
    {
        BasePrice = newBasePrice;
        UpdatedAt = DateTime.Now;
    }
}
