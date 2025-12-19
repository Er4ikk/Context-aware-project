using System.ComponentModel.DataAnnotations.Schema;

namespace smartparking.db.parkingevent;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

public class ParkingEvent{
    public ParkingEvent()
    {
    }

    public ParkingEvent(int ParkingAreaId, DateTimeOffset TimeStamp, string EventType)
    {
        this.ParkingAreaId = ParkingAreaId;
        this.TimeStamp = TimeStamp;
        this.EventType = EventType;
    }
     [Column("id")]
    public int Id{get;set;}

    [Column("ParkingAreaId")]
    public int ParkingAreaId { get; set; }
    [Column("Timestamp")]
    public DateTimeOffset TimeStamp { get; set; }
    [Column("EventType")]
    public string EventType{ get; set; }


}