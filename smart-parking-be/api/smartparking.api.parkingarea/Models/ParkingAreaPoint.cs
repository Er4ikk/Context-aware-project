using System.Text.Json.Serialization;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using smartparking.db.parkingarea;

namespace parkingCenter.api.parkingCenter;

public class ParkingAreaCentroid
{

    [JsonConstructor]
    public ParkingAreaCentroid(String? Center, int MaxCapacity,int PlacesLeft)
    {
        this.Center=Center;
        this.MaxCapacity = MaxCapacity;
        this.PlacesLeft = PlacesLeft;
    }


    public ParkingAreaCentroid(ParkingArea parkingArea)
    {

        this.Id = parkingArea.Id;
       
        this.Center = geoJsonWriter.Write(parkingArea.Area?.Centroid);
        this.MaxCapacity = parkingArea.MaxCapacity;
        this.PlacesLeft =parkingArea.PlacesLeft;

    }

    public ParkingAreaCentroid()
    {
    }

    // [JsonIgnore]
    public int Id{get;set;}

    public String? Center {get;set;}

    public int MaxCapacity {get;set;}
    public int PlacesLeft {get;set;}
    private GeoJsonWriter geoJsonWriter = new GeoJsonWriter();
    private GeoJsonReader geoJsonReader = new GeoJsonReader();
    // public ParkingAreaCentroid Convert()
    // {
    //     ParkingAreaCentroid parkingAreaCentroid = new ParkingAreaCentroid();
    //     // ParkingAreaCentroid.Id = this.Id;
    //     if(this.Center!=null)
    //         parkingAreaCentroid.Center = geoJsonReader.Read<Point>(this.Center);
    //     parkingAreaCentroid.MaxCapacity = this.MaxCapacity;
    //     parkingAreaCentroid.PlacesLeft = this.PlacesLeft;
    //     return parkingAreaCentroid;
    // }
    
}