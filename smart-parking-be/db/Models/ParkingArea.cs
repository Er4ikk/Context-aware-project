using System.ComponentModel.DataAnnotations.Schema;

namespace smartparking.db.parkingarea;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

public class ParkingArea{
    public ParkingArea()
    {
    }

    public ParkingArea(NetTopologySuite.Geometries.Geometry Area, int Capacity)
    {   
        this.Id = new Random().Next();
        this.Area=Area;
        this.Capacity = Capacity;
    }

    public int Id{get;set;}
    [Column("area")]
    public NetTopologySuite.Geometries.Geometry Area {get;set;}
     [Column("capacity")]
    public int Capacity {get;set;}

}