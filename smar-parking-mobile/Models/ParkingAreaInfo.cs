using System.Text.Json.Serialization;

namespace smar_parking_mobile.Models;

public class ParkingAreaInfo
{

    [JsonConstructor]
    public ParkingAreaInfo(String? Area, int MaxCapacity,int PlacesLeft)
    {
        this.Area=Area;
        this.MaxCapacity = MaxCapacity;
        this.PlacesLeft = PlacesLeft;
    }



    // [JsonIgnore]
    public int Id{get;set;}

    public String? Area {get;set;}

    public int MaxCapacity {get;set;}
    public int PlacesLeft {get;set;}
 
}