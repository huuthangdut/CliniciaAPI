using Clinicia.Dtos.Common;

namespace Clinicia.Services.Interfaces
{
    public interface ILocationService
    {
        MapAddress GetAddressFromLatLng(double lat, double lng);

        MapPoint GetLatLngFromAddress(string address);
    }
}
