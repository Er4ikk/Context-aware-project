using System.Text.Json.Serialization;
using NetTopologySuite.Geometries;
using smartparking.db.parkingevent;

namespace parkingEvent.api.parkingEvent;

public class ParkingEventInfo
{

    [JsonConstructor]
    public ParkingEventInfo(int ParkingAreaId, string EventType, int UserId, Coordinates? ParkingCoordinates)
    {
        // this.Id = new Random().Next();
        this.ParkingAreaId = ParkingAreaId;
        this.TimeStamp =DateTimeOffset.UtcNow;
        this.EventType = EventType;
        this.UserId = UserId;
        this.ParkingCoordinates = ParkingCoordinates;
        
    }


    public ParkingEventInfo(ParkingEvent parkingEvent)
    {

        this.Id = parkingEvent.Id;
        this.ParkingAreaId = parkingEvent.ParkingAreaId;
        this.TimeStamp = parkingEvent.TimeStamp;
        this.EventType = parkingEvent.EventType;
        this.UserId = parkingEvent.UserId;

        if(parkingEvent.ParkingCoordinates!= null)
        {
            
            this.ParkingCoordinates = new Coordinates(
                parkingEvent.ParkingCoordinates.Coordinate.X,
                parkingEvent.ParkingCoordinates.Coordinate.Y);
        }
        else
        {
            this.ParkingCoordinates = null;
        }

    }

    public int Id { get; set; }

    public int ParkingAreaId { get; set; }
    public DateTimeOffset TimeStamp { get; set; }
    public string EventType { get; set; }

    public int UserId { get; set; }


    public Coordinates? ParkingCoordinates { get; set; }

    public ParkingEvent Convert()
    {
        ParkingEvent parkingEvent = new ParkingEvent();
        parkingEvent.Id = this.Id;
        parkingEvent.ParkingAreaId = this.ParkingAreaId;
        parkingEvent.TimeStamp = DateTimeOffset.UtcNow;
        parkingEvent.EventType = this.EventType;
        parkingEvent.UserId = this.UserId;
        if(this.ParkingCoordinates!= null)
        {

            parkingEvent.ParkingCoordinates = new Point(new Coordinate(this.ParkingCoordinates.x,this.ParkingCoordinates.y));
        }
        else
        {
            parkingEvent.ParkingCoordinates = null;
        }
        return parkingEvent;
    }

}