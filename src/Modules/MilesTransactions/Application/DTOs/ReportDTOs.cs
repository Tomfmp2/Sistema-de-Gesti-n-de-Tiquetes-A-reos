namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.MilesTransactions.Application.DTOs;

public class ClientMilesDTO
{
    public int ClientId { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public decimal TotalAccumulated { get; set; }
}

public class ClientRedemptionDTO
{
    public int ClientId { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public decimal TotalRedeemed { get; set; }
}

public class AirlineLoyaltyDTO
{
    public int AirlineId { get; set; }
    public string AirlineName { get; set; } = string.Empty;
    public decimal TotalMilesGranted { get; set; }
}

public class RouteMilesDTO
{
    public string Origin { get; set; } = string.Empty;
    public string Destination { get; set; } = string.Empty;
    public decimal TotalMiles { get; set; }
}

public class FrequentFlyerDTO
{
    public int ClientId { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public int CompletedFlights { get; set; }
    public decimal CurrentBalance { get; set; }
}
