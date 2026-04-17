namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Domain.ValueObjet
{
    public class CityRegionId
    {
        public int Value { get; }

        public CityRegionId(int value)
        {
            Value = value;
        }

        public static CityRegionId Create(int value)
        {
            if (value <= 0)
            {
                throw new ArgumentException("El valor no puede ser menor a 1");
            }

            return new CityRegionId(value);
        }
    }
}
