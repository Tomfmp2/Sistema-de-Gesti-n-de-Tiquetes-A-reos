namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Infrastructure.Entity;

public class PassengerTypeEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int? MinAge { get; set; }
    public int? MaxAge { get; set; }
}
