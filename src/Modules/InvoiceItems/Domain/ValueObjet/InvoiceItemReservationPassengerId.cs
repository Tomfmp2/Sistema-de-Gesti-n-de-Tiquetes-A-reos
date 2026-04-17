namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItems.Domain.ValueObjet;

public class InvoiceItemReservationPassengerId
{
    public int? Value { get; }

    public InvoiceItemReservationPassengerId(int? value)
    {
        Value = value;
    }

    public static InvoiceItemReservationPassengerId Create(int? value)
    {
        if (value.HasValue && value.Value <= 0)
        {
            throw new ArgumentException("El valor no puede ser menor a 1");
        }

        return new InvoiceItemReservationPassengerId(value);
    }
}
