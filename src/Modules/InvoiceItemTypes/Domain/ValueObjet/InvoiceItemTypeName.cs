namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItemTypes.Domain.ValueObjet;

public class InvoiceItemTypeName
{
    public string Value { get; }

    public InvoiceItemTypeName(string value)
    {
        Value = value;
    }

    public static InvoiceItemTypeName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("El valor no puede ser nulo ni vacio");
        }

        if (value.Trim().Length > 100)
        {
            throw new ArgumentException("El valor no puede tener mas de 100 caracteres");
        }

        return new InvoiceItemTypeName(value.Trim());
    }
}
