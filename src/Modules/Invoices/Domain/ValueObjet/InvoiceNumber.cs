namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.Domain.ValueObjet;

public class InvoiceNumber
{
    public string Value { get; }

    public InvoiceNumber(string value)
    {
        Value = value;
    }

    public static InvoiceNumber Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("El valor no puede ser nulo ni vacio");
        }

        if (value.Trim().Length > 30)
        {
            throw new ArgumentException("El valor no puede tener mas de 30 caracteres");
        }

        return new InvoiceNumber(value.Trim());
    }
}
