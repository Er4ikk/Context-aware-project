using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using NetTopologySuite.Geometries;

namespace smartparking.db.parkingarea;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

public class ParkingArea{
    public ParkingArea()
    {
    }

    public ParkingArea(Polygon? Area, int MaxCapacity,int PlacesLeft)
    {   
        this.Id = new Random().Next();
        this.Area=Area;
        this.MaxCapacity = MaxCapacity;
        this.PlacesLeft = PlacesLeft;

    }

    
    public int Id{get;set;}
    [Column("Area")]
    public Polygon? Area {get;set;}
     [Column("MaxCapacity")]
    public int MaxCapacity {get;set;}
    [Column("PlacesLeft")]
    public int PlacesLeft {get;set;}

}