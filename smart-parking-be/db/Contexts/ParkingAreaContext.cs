

using Microsoft.EntityFrameworkCore;

namespace smartparking.db.parkingarea;

public class ParkingAreaContext:PostGresContext
{


    public ParkingAreaContext(IConfiguration configuration) : base(configuration)
    {
    }

    public List<ParkingArea> GetParkingAreas()
    {
        //  _logger.LogInformation("Getting ParkingAreas");
        List<ParkingArea> ParkingAreas = ParkingArea
        .Where(p => p.Area != null)
        .ToList();

        return ParkingAreas;
    }
     //fix update -> https://stackoverflow.com/questions/48202403/instance-of-entity-type-cannot-be-tracked-because-another-instance-with-same-key
    public ParkingArea GetParkingAreaById(int id)
    {
        ParkingArea parkingArea = ParkingArea.AsNoTracking()
        .Where(p => p.Area != null)
        .Where((ParkingArea) => ParkingArea.Id == id)
        .AsEnumerable()
        .First();

        return parkingArea;
    }

    public async Task CreateParkingArea(ParkingArea parkingArea)
    {
        await ParkingArea.AddAsync(parkingArea);
        await SaveChangesAsync();
    }


    public async Task UpdateParkingArea(ParkingArea parkingArea)
    {
        ParkingArea.Update(parkingArea);
        await SaveChangesAsync();
    }

    public async Task ReducePlaceAvailable(int parkingAreaId)
    {
        ParkingArea parkingArea = ParkingArea.AsNoTracking()
        .Where(p => p.Area != null)
        .Where((ParkingArea) => ParkingArea.Id == parkingAreaId)
        .AsEnumerable()
        .First();

        if(parkingArea.PlacesLeft > 0)
            parkingArea.PlacesLeft-=1;
        ParkingArea.Update(parkingArea);

        await SaveChangesAsync();
    }

     public async Task AddPlaceAvailable(int parkingAreaId)
    {
        ParkingArea parkingArea = ParkingArea.AsNoTracking()
        .Where(p => p.Area != null)
        .Where((ParkingArea) => ParkingArea.Id == parkingAreaId)
        .AsEnumerable()
        .First();

        if(parkingArea.PlacesLeft < parkingArea.MaxCapacity)
            parkingArea.PlacesLeft+=1;
        ParkingArea.Update(parkingArea);

        await SaveChangesAsync();
    }


    public async Task DeleteParkingArea(ParkingArea parkingArea)
    {
        ParkingArea.Remove(parkingArea);
        await SaveChangesAsync();
    }

    public async Task DeleteParkingAreaById(int id)
    {
        ParkingArea ParkingAreaToDelete = new ParkingArea() { Id = id };
        Entry(ParkingAreaToDelete).State = EntityState.Deleted;
        await SaveChangesAsync();
    }
}