using Clinicia.Common.Helpers;
using Clinicia.Dtos.Common;
using GeoCoordinatePortable;
using GoogleMaps.LocationServices;
using Microsoft.Extensions.Options;
using ILocationService = Clinicia.Services.Interfaces.ILocationService;
using MapPoint = Clinicia.Dtos.Common.MapPoint;

namespace Clinicia.Services.Implementations
{
    public class LocationService : ILocationService
    {
        private readonly AppSettings _appSettings;

        public LocationService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public MapAddress GetAddressFromLatLng(double lat, double lng)
        {
            var gls = new GoogleLocationService(_appSettings.GoogleApiKey);
            var addressData = gls.GetAddressFromLatLang(lat, lng);

            return new MapAddress
            {
                Address = addressData.Address,
                City = addressData.City,
                Country = addressData.Country,
                Zip = addressData.Zip
            };
        }

        public MapPoint GetLatLngFromAddress(string address)
        {
            var gls = new GoogleLocationService(_appSettings.GoogleApiKey);
            var pointData = gls.GetLatLongFromAddress(address);

            return new MapPoint
            {
                Latitude = pointData.Latitude,
                Longitude =  pointData.Longitude
            };
        }
    }
}
