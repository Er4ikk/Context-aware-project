using System.Text.Json.Serialization;

public class Coordinates
{

    [JsonConstructor]
    public Coordinates(double x, double y)
    {
        this.x = x;
        this.y = y;
    }
    public double x { get; set; }
    public double y { get; set; }

}