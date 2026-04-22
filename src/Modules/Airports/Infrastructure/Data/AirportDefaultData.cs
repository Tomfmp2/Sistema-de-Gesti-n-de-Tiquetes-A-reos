using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Infrastructure.Data;

public static class AirportDefaultData
{
    public static readonly AirportEntity[] Airports =
    [
        new() { Id = 1, Name = "Aeropuerto Internacional El Dorado", IataCode = "BOG", IcaoCode = "SKBO", CityId = 1 },
        new() { Id = 2, Name = "Aeropuerto Internacional Jose Maria Cordova", IataCode = "MDE", IcaoCode = "SKRG", CityId = 2 },
        new() { Id = 3, Name = "Aeropuerto Internacional Alfonso Bonilla Aragon", IataCode = "CLO", IcaoCode = "SKCL", CityId = 3 },
        new() { Id = 4, Name = "Aeropuerto Internacional Ernesto Cortissoz", IataCode = "BAQ", IcaoCode = "SKBQ", CityId = 4 },
        new() { Id = 5, Name = "Miami International Airport", IataCode = "MIA", IcaoCode = "KMIA", CityId = 5 },
        new() { Id = 6, Name = "John F. Kennedy International Airport", IataCode = "JFK", IcaoCode = "KJFK", CityId = 6 },
        new() { Id = 7, Name = "Los Angeles International Airport", IataCode = "LAX", IcaoCode = "KLAX", CityId = 7 },
        new() { Id = 8, Name = "Aeropuerto Internacional Benito Juarez", IataCode = "MEX", IcaoCode = "MMMX", CityId = 8 },
        new() { Id = 9, Name = "Aeropuerto Adolfo Suarez Madrid-Barajas", IataCode = "MAD", IcaoCode = "LEMD", CityId = 9 },
        new() { Id = 10, Name = "Paris Charles de Gaulle Airport", IataCode = "CDG", IcaoCode = "LFPG", CityId = 10 },
        new() { Id = 11, Name = "London Heathrow Airport", IataCode = "LHR", IcaoCode = "EGLL", CityId = 11 },
        new() { Id = 12, Name = "Sao Paulo Guarulhos International Airport", IataCode = "GRU", IcaoCode = "SBGR", CityId = 12 },
        new() { Id = 13, Name = "Aeropuerto Internacional Ezeiza", IataCode = "EZE", IcaoCode = "SAEZ", CityId = 13 },
        new() { Id = 14, Name = "Aeropuerto Internacional Arturo Merino Benitez", IataCode = "SCL", IcaoCode = "SCEL", CityId = 14 },
        new() { Id = 15, Name = "Aeropuerto Internacional Jorge Chavez", IataCode = "LIM", IcaoCode = "SPJC", CityId = 15 },
        new() { Id = 16, Name = "Toronto Pearson International Airport", IataCode = "YYZ", IcaoCode = "CYYZ", CityId = 16 },
        new() { Id = 17, Name = "Tokyo Haneda Airport", IataCode = "HND", IcaoCode = "RJTT", CityId = 17 },
        new() { Id = 18, Name = "Sydney Kingsford Smith Airport", IataCode = "SYD", IcaoCode = "YSSY", CityId = 18 }
    ];
}
