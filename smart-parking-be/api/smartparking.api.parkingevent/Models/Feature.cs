public class Geometry
{
    public List<double> Coordinates { get; set; }
    public string Type { get; set; }

    public Geometry(string type, Coordinates  coordinates)
    {
        List<double> coordinatesToDoubles = new List<double>();
        coordinatesToDoubles.Add(coordinates.x);
        coordinatesToDoubles.Add(coordinates.y);
        this.Coordinates = coordinatesToDoubles;

        this.Type = type;
    }
}

public class Properties
{
    
}



public class Feature
{
    public string Type { get; set; }
    public Geometry Geometry { get; set; }

    public Properties Properties {get;set;}

    public Feature(string type, Properties properties, Geometry geometry)
    {
        this.Geometry = geometry;
        this.Properties =properties;

        this.Type = type;
    }
}