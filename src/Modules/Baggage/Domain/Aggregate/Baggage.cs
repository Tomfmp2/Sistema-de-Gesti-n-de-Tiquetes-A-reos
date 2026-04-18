using sistema_gestor_de_tiquetes_aereos.Src.Modules.Baggage.Domain.ValueObjects;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Baggage.Domain.Aggregate;

public sealed class BaggageItem
{
    public BaggageId Id { get; private set; }
    public int CheckinId { get; private set; }
    public int BaggageTypeId { get; private set; }
    public WeightKg WeightKg { get; private set; }
    public ChargedPrice ChargedPrice { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private BaggageItem() { }

    public static BaggageItem Create(
        int checkinId,
        int baggageTypeId,
        WeightKg weightKg,
        ChargedPrice chargedPrice)
    {
        return new BaggageItem
        {
            Id = null!,
            CheckinId = checkinId,
            BaggageTypeId = baggageTypeId,
            WeightKg = weightKg,
            ChargedPrice = chargedPrice,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
    }

    public static BaggageItem Reconstitute(
        int id,
        int checkinId,
        int baggageTypeId,
        decimal weightKg,
        decimal chargedPrice,
        DateTime createdAt,
        DateTime updatedAt)
    {
        return new BaggageItem
        {
            Id = BaggageId.Reconstitute(id),
            CheckinId = checkinId,
            BaggageTypeId = baggageTypeId,
            WeightKg = WeightKg.Reconstitute(weightKg),
            ChargedPrice = ChargedPrice.Reconstitute(chargedPrice),
            CreatedAt = createdAt,
            UpdatedAt = updatedAt
        };
    }

    public void UpdateWeightKg(WeightKg newWeightKg)
    {
        WeightKg = newWeightKg;
        UpdatedAt = DateTime.Now;
    }

    public void UpdateChargedPrice(ChargedPrice newChargedPrice)
    {
        ChargedPrice = newChargedPrice;
        UpdatedAt = DateTime.Now;
    }

    public void UpdateBaggageTypeId(int newBaggageTypeId)
    {
        BaggageTypeId = newBaggageTypeId;
        UpdatedAt = DateTime.Now;
    }
}
