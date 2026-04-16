namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItems.Domain.ValueObjet;

public class InvoiceItemUnitPrice
{
    public decimal Value { get; }

    public InvoiceItemUnitPrice(decimal value)
    {
        Value = value;
    }

    public static InvoiceItemUnitPrice Create(decimal value)
    {
        if (value < 0)
        {
            throw new ArgumentException("El precio unitario no puede ser menor a 0");
        }

        return new InvoiceItemUnitPrice(value);
    }
}
