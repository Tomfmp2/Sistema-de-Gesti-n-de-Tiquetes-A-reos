namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItems.Domain.ValueObjet;

public class InvoiceItemQuantity
{
    public int Value { get; }

    public InvoiceItemQuantity(int value)
    {
        Value = value;
    }

    public static InvoiceItemQuantity Create(int value)
    {
        if (value < 1)
        {
            throw new ArgumentException("La cantidad no puede ser menor a 1");
        }

        return new InvoiceItemQuantity(value);
    }
}
