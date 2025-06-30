using System.ComponentModel.DataAnnotations.Schema;

namespace smartparking.db.parkingevent;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

public class ParkingEvent{
    public ParkingEvent()
    {
    }

    public ParkingEvent(int ParkingAreaId, string TimeStamp, string EventType)
    {
        this.Id = new Random().Next();
        this.ParkingAreaId = ParkingAreaId;
        this.TimeStamp = TimeStamp;
        this.EventType = EventType;
    }

    public int Id{get;set;}

    [Column("parkingAreaId")]
    public int ParkingAreaId { get; set; }
    [Column("timestamp")]
    public string TimeStamp { get; set; }
    [Column("eventType")]
    public string EventType{ get; set; }


}