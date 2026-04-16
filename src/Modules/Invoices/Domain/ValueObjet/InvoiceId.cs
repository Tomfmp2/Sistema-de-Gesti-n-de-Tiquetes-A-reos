namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.Domain.ValueObjet;

public class InvoiceId
{
    public int Value { get; }

    public InvoiceId(int value)
    {
        Value = value;
    }

    public static InvoiceId Create(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("El valor no puede ser menor a 1");
        }

        return new InvoiceId(value);
    }
}
