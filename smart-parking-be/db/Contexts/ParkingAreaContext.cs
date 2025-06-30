

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
        .ToList();

        return ParkingAreas;
    }
     //fix update -> https://stackoverflow.com/questions/48202403/instance-of-entity-type-cannot-be-tracked-because-another-instance-with-same-key
    public ParkingArea GetParkingAreaById(int id)
    {
        ParkingArea parkingArea = ParkingArea.AsNoTracking()
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