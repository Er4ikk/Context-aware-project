using System.Text.Json.Serialization;
using smartparking.db.parkingarea;

namespace parkingArea.api.parkingArea;

public class ParkingAreaInfo
{

    [JsonConstructor]
    public ParkingAreaInfo(NetTopologySuite.Geometries.Geometry Area, int Capacity)
    {
        this.Area=Area;
        this.Capacity = Capacity;
    }


    public ParkingAreaInfo(ParkingArea parkingArea)
    {

        this.Id = parkingArea.Id;
        this.Area = parkingArea.Area;
        this.Capacity = parkingArea.Capacity;

    }

  
    public int Id{get;set;}

    public NetTopologySuite.Geometries.Geometry Area {get;set;}

    public int Capacity {get;set;}
    public ParkingArea Convert()
    {
        ParkingArea parkingArea = new ParkingArea();
        parkingArea.Id = this.Id;
        parkingArea.Area = this.Area;
        parkingArea.Capacity = this.Capacity;
        return parkingArea;
    }
    
}