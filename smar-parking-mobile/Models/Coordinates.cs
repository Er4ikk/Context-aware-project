using System.Text.Json.Serialization;
namespace smar_parking_mobile.Models;
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