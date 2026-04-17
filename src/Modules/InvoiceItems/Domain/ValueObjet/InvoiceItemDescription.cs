namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItems.Domain.ValueObjet;

public class InvoiceItemDescription
{
    public string Value { get; }

    public InvoiceItemDescription(string value)
    {
        Value = value;
    }

    public static InvoiceItemDescription Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("El valor no puede ser nulo ni vacio");
        }

        if (value.Trim().Length > 200)
        {
            throw new ArgumentException("El valor no puede tener mas de 200 caracteres");
        }

        return new InvoiceItemDescription(value.Trim());
    }
}
