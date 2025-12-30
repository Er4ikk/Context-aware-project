using System.Text.Json.Serialization;

namespace smar_parking_mobile.Models;

public class ParkingEventInfo
{

    [JsonConstructor]
    public ParkingEventInfo(int ParkingAreaId, string EventType, int UserId, Coordinates? ParkingCoordinates)
    {
        // this.Id = new Random().Next();
        this.ParkingAreaId = ParkingAreaId;
        this.TimeStamp = DateTimeOffset.Now;
        this.EventType = EventType;
        this.UserId = UserId;
        this.ParkingCoordinates = ParkingCoordinates;
        
    }

    public ParkingEventInfo()
    {
        
    }


    
    public int Id { get; set; }

    public int ParkingAreaId { get; set; }
    public DateTimeOffset TimeStamp { get; set; }
    public string EventType { get; set; }

    public int UserId { get; set; }


    public Coordinates? ParkingCoordinates { get; set; }

   

}