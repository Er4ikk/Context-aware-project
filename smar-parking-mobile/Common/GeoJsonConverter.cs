
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using Location = Microsoft.Maui.Devices.Sensors.Location;

namespace smar_parking_mobile.Common;

public static class GeoJsonConverterMaui
{
    public static Geometry ConvertGeoJsonToMauiGeometry(string geoJson)
    {
        var serializer = new GeoJsonReader();
        
        Geometry geometry = serializer.Read<Geometry>(geoJson);

        return geometry;
    }
    public static List<Location> ConvertGeoJsonToMauiLocations(string geoJson)
    {
        var serializer = new GeoJsonReader();
        var geometry = serializer.Read<Geometry>(geoJson);

        var locations = new List<Location>();

        if (geometry is Polygon polygon)
        {
            foreach (var coord in polygon.ExteriorRing.Coordinates)
            {
                locations.Add(new Location(coord.Y, coord.X));
            }
        }

        return locations;
    }

}
