

using Microsoft.EntityFrameworkCore;
using smartparking.db.parkingevent;

namespace smartparking.db.parkingarea;

public class ParkingEventContext:PostGresContext
{


    public ParkingEventContext(IConfiguration configuration) : base(configuration)
    {
    }

    public List<ParkingEvent> GetParkingEvents()
    {
        //  _logger.LogInformation("Getting ParkingEvents");
        List<ParkingEvent> ParkingEvents = ParkingEvent
        .ToList();

        return ParkingEvents;
    }
     //fix update -> https://stackoverflow.com/questions/48202403/instance-of-entity-type-cannot-be-tracked-because-another-instance-with-same-key
    public ParkingEvent GetParkingEventById(int id)
    {
        ParkingEvent parkingEvent = ParkingEvent.AsNoTracking()
        .Where((ParkingEvent) => ParkingEvent.Id == id)
        .AsEnumerable()
        .First();

        return parkingEvent;
    }

    public List<ParkingEvent> GetParkingEventsByParkingAreaId(int id)
    {
        List<ParkingEvent> parkingEvent = ParkingEvent.AsNoTracking()
        .Where((ParkingEvent) => ParkingEvent.ParkingAreaId == id)
        .AsEnumerable()
        .Take(10)
        .OrderByDescending(parkingEvent => parkingEvent.TimeStamp)
        .ToList();

        return parkingEvent;
    }

     public List<ParkingEvent> GetParkingEventsByUserId(int UserId)
    {
        List<ParkingEvent> parkingEvent = ParkingEvent.AsNoTracking()
        .Where((ParkingEvent) => ParkingEvent.UserId == UserId)
        .AsEnumerable()
        .Take(10)
        .OrderByDescending(parkingEvent => parkingEvent.TimeStamp)
        .ToList();

        return parkingEvent;
    }
    public List<ParkingEvent> GetParkingEventsByTimeRange(DateTimeOffset start, DateTimeOffset end)
    {
        DateTimeOffset startUtc = start.ToUniversalTime();
        DateTimeOffset endUtc = end.ToUniversalTime();
        
        //https://learn.microsoft.com/en-us/ef/core/querying/client-eval -> not use DateTimeOffset.CompareTO() or DateTimeOffset.toUniversalTIme()
         List<ParkingEvent> parkingEvents = ParkingEvent.AsNoTracking()
        .Where((ParkingEvent) => 
        ParkingEvent.TimeStamp >= startUtc &&
        ParkingEvent.TimeStamp <= endUtc)
        .ToList();

        return parkingEvents;
    }

    public async Task CreateParkingEvent(ParkingEvent parkingEvent)
    {
        await ParkingEvent.AddAsync(parkingEvent);
        await SaveChangesAsync();
    }


    public async Task UpdateParkingEvent(ParkingEvent parkingEvent)
    {
        ParkingEvent.Update(parkingEvent);
        await SaveChangesAsync();
    }


    public async Task DeleteParkingEvent(ParkingEvent parkingEvent)
    {
        ParkingEvent.Remove(parkingEvent);
        await SaveChangesAsync();
    }

    public async Task DeleteParkingEventById(int id)
    {
        ParkingEvent ParkingEventToDelete = new ParkingEvent() { Id = id };
        Entry(ParkingEventToDelete).State = EntityState.Deleted;
        await SaveChangesAsync();
    }
}