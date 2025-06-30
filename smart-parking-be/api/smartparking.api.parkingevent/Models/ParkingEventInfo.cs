using System.Text.Json.Serialization;
using smartparking.db.parkingevent;

namespace parkingEvent.api.parkingEvent;

public class ParkingEventInfo
{

    [JsonConstructor]
    public ParkingEventInfo(int ParkingAreaId, string TimeStamp, string EventType)
    {
        // this.Id = new Random().Next();
        this.ParkingAreaId = ParkingAreaId;
        this.TimeStamp = TimeStamp;
        this.EventType = EventType;
    }


    public ParkingEventInfo(ParkingEvent parkingEvent)
    {

        this.Id = parkingEvent.Id;
        this.ParkingAreaId = parkingEvent.ParkingAreaId;
        this.TimeStamp = parkingEvent.TimeStamp;
        this.EventType = parkingEvent.EventType;

    }

    public int Id{get;set;}

    public int ParkingAreaId { get; set; }
    public string TimeStamp { get; set; }
    public string EventType{ get; set; }

    public ParkingEvent Convert()
    {
        ParkingEvent parkingEvent = new ParkingEvent();
        parkingEvent.Id = this.Id;
        parkingEvent.ParkingAreaId = this.ParkingAreaId;
        parkingEvent.TimeStamp = this.TimeStamp;
        parkingEvent.EventType = this.EventType;
        return parkingEvent;
    }
    
}