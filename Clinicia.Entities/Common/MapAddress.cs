namespace Clinicia.Dtos.Common
{
    public class MapAddress
    {
        public string Address { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Zip { get; set; }

        public string FormattedAddress => $"{Address}, {City}, {Zip}, {Country}";
    }
}
