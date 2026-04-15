namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Domain.ValueObjet
{
    public sealed record CityId
    {
        public int Value { get; }

        public CityId(int value)
        {
            Value = value;
        }

        public static CityId Create(int value)
        {
            if (value <= 0)
            {
                throw new ArgumentException("El valor no puede ser menor a 1");
            }

            return new CityId(value);
        }
    }
}
