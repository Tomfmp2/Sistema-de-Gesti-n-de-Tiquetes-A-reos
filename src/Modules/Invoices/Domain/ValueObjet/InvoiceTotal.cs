namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.Domain.ValueObjet;

public class InvoiceTotal
{
    public decimal Value { get; }

    public InvoiceTotal(decimal value)
    {
        Value = value;
    }

    public static InvoiceTotal Create(decimal value)
    {
        if (value < 0)
        {
            throw new ArgumentException("El total no puede ser menor a 0");
        }

        return new InvoiceTotal(value);
    }
}
