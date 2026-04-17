namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Domain.ValueObjet
{
    public sealed record CityName
    {
        public string Value { get; }

        public CityName(string value)
        {
            Value = value;
        }

        public static CityName Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("El valor no puede ser nulo ni vacío");
            }

            if (value.Length > 100)
            {
                throw new ArgumentException("El valor no puede tener más de 100 caracteres");
            }

            return new CityName(value.Trim());
        }
    }
}
