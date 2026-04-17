namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItems.Domain.ValueObjet;

public class InvoiceItemTypeId
{
    public int Value { get; }

    public InvoiceItemTypeId(int value)
    {
        Value = value;
    }

    public static InvoiceItemTypeId Create(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("El valor no puede ser menor a 1");
        }

        return new InvoiceItemTypeId(value);
    }
}
