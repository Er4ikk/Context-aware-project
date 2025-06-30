

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