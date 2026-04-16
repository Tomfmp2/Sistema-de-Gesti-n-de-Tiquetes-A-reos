namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.Domain.ValueObjet;

public class InvoiceSubtotal
{
    public decimal Value { get; }

    public InvoiceSubtotal(decimal value)
    {
        Value = value;
    }

    public static InvoiceSubtotal Create(decimal value)
    {
        if (value < 0)
        {
            throw new ArgumentException("El subtotal no puede ser menor a 0");
        }

        return new InvoiceSubtotal(value);
    }
}
