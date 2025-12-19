using System.Text.Json.Serialization;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using smartparking.db.parkingarea;

namespace parkingArea.api.parkingArea;

public class ParkingAreaInfo
{

    [JsonConstructor]
    public ParkingAreaInfo(String? Area, int MaxCapacity,int PlacesLeft)
    {
        this.Area=Area;
        this.MaxCapacity = MaxCapacity;
        this.PlacesLeft = PlacesLeft;
    }


    public ParkingAreaInfo(ParkingArea parkingArea)
    {

        this.Id = parkingArea.Id;
       
        this.Area = geoJsonWriter.Write(parkingArea.Area);
        this.MaxCapacity = parkingArea.MaxCapacity;
        this.PlacesLeft =parkingArea.PlacesLeft;

    }

  
    public int Id{get;set;}

    public String? Area {get;set;}

    public int MaxCapacity {get;set;}
    public int PlacesLeft {get;set;}
    private GeoJsonWriter geoJsonWriter = new GeoJsonWriter();
    private GeoJsonReader geoJsonReader = new GeoJsonReader();
    public ParkingArea Convert()
    {
        ParkingArea parkingArea = new ParkingArea();
        parkingArea.Id = this.Id;
        if(this.Area!=null)
            parkingArea.Area = geoJsonReader.Read<Polygon?>(this.Area);
        parkingArea.MaxCapacity = this.MaxCapacity;
        parkingArea.PlacesLeft = this.PlacesLeft;
        return parkingArea;
    }
    
}