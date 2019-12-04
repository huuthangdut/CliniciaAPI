namespace Clinicia.Dtos.Input
{
    public class UserLocation
    {
        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public string FormattedAddress { get; set; }
    }
}