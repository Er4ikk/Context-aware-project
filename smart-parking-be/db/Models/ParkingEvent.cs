using System.ComponentModel.DataAnnotations.Schema;
using NetTopologySuite.Geometries;

namespace smartparking.db.parkingevent;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

public class ParkingEvent{
    public ParkingEvent()
    {
    }

    public ParkingEvent(int ParkingAreaId, DateTimeOffset TimeStamp, string EventType,int UserId,Point? ParkingCoordinates)
    {
        this.ParkingAreaId = ParkingAreaId;
        this.TimeStamp = TimeStamp;
        this.EventType = EventType;
        this.UserId = UserId;
        this.ParkingCoordinates = ParkingCoordinates;
    }
     [Column("id")]
    public int Id{get;set;}

    [Column("ParkingAreaId")]
    public int ParkingAreaId { get; set; }
    [Column("Timestamp")]
    public DateTimeOffset TimeStamp { get; set; }
    [Column("EventType")]
    public string EventType{ get; set; }

    [Column("UserId")]
    public int UserId {get;set;}

    //https://github.com/NetTopologySuite/NetTopologySuite/wiki/GettingStarted -> Point
    [Column("ParkingCoordinates")]
    public Point? ParkingCoordinates {get;set;}


}