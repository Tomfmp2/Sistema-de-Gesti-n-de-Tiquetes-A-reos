namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.Domain.ValueObjet;

public class InvoiceCreatedAt
{
    public DateTime Value { get; }

    public InvoiceCreatedAt(DateTime value)
    {
        Value = value;
    }

    public static InvoiceCreatedAt Create(DateTime value)
    {
        if (value == default)
        {
            throw new ArgumentException("La fecha de creacion no puede ser vacia");
        }

        return new InvoiceCreatedAt(value);
    }
}
