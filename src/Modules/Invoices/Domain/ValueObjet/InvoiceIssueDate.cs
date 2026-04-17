namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.Domain.ValueObjet;

public class InvoiceIssueDate
{
    public DateTime Value { get; }

    public InvoiceIssueDate(DateTime value)
    {
        Value = value;
    }

    public static InvoiceIssueDate Create(DateTime value)
    {
        if (value == default)
        {
            throw new ArgumentException("La fecha de emision no puede ser vacia");
        }

        return new InvoiceIssueDate(value);
    }
}
