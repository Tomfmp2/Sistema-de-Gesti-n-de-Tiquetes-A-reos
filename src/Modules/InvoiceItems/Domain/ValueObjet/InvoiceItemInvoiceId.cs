namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItems.Domain.ValueObjet;

public class InvoiceItemInvoiceId
{
    public int Value { get; }

    public InvoiceItemInvoiceId(int value)
    {
        Value = value;
    }

    public static InvoiceItemInvoiceId Create(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("El valor no puede ser menor a 1");
        }

        return new InvoiceItemInvoiceId(value);
    }
}
