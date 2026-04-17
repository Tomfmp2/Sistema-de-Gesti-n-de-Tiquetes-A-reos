namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.Domain.ValueObjet;

public class InvoiceTaxes
{
    public decimal Value { get; }

    public InvoiceTaxes(decimal value)
    {
        Value = value;
    }

    public static InvoiceTaxes Create(decimal value)
    {
        if (value < 0)
        {
            throw new ArgumentException("Los impuestos no pueden ser menores a 0");
        }

        return new InvoiceTaxes(value);
    }
}
