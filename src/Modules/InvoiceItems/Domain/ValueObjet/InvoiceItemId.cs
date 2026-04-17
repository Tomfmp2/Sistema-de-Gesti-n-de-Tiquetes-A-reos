namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItems.Domain.ValueObjet;

public class InvoiceItemId
{
    public int Value { get; }

    public InvoiceItemId(int value)
    {
        Value = value;
    }

    public static InvoiceItemId Create(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("El valor no puede ser menor a 1");
        }

        return new InvoiceItemId(value);
    }
}
