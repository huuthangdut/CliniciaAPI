using GeoCoordinatePortable;

namespace Clinicia.Common.Helpers
{
    public static class LocationHelper
    {
        public static double GetDistance(double lat1, double lng1, double lat2, double lng2)
        {
            var point1 = new GeoCoordinate(lat1, lng1);
            var point2 = new GeoCoordinate(lat2, lng2);

            return point1.GetDistanceTo(point2);
        }

        public static decimal GetDistance(decimal lat1, decimal lng1, decimal lat2, decimal lng2)
        {
            var point1 = new GeoCoordinate((double)lat1, (double)lng1);
            var point2 = new GeoCoordinate((double)lat2, (double)lng2);

            return (decimal)point1.GetDistanceTo(point2);
        }
    }
}
