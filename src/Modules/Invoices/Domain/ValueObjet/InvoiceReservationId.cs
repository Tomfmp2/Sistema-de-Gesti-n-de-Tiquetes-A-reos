namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.Domain.ValueObjet;

public class InvoiceReservationId
{
    public int Value { get; }

    public InvoiceReservationId(int value)
    {
        Value = value;
    }

    public static InvoiceReservationId Create(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("El valor no puede ser menor a 1");
        }

        return new InvoiceReservationId(value);
    }
}
