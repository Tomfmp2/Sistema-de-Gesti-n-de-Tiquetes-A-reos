namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Infrastructure.Entity;

public class PaymentMethodEntity
{
    public int Id { get; set; }
    public int PaymentMethodTypeId { get; set; }
    public int? CardTypeId { get; set; }
    public int? CardIssuerId { get; set; }
    public string? CommercialName { get; set; }
}
