namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItems.Domain.ValueObjet;

public class InvoiceItemSubtotal
{
    public decimal Value { get; }

    public InvoiceItemSubtotal(decimal value)
    {
        Value = value;
    }

    public static InvoiceItemSubtotal Create(decimal value)
    {
        if (value < 0)
        {
            throw new ArgumentException("El subtotal no puede ser menor a 0");
        }

        return new InvoiceItemSubtotal(value);
    }
}
