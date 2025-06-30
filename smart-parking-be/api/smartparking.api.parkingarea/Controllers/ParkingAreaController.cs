
using Microsoft.AspNetCore.Mvc;
using smartparking.db.parkingarea;
using smartparking.db.postgres;

namespace parkingArea.api.parkingArea
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ParkingAreaController : ControllerBase
    {

        private readonly PostGresClient _parkingAreaClient;
        private readonly ILogger<ParkingAreaController> _logger;

        public ParkingAreaController(
            PostGresClient parkingAreaClient,
            ILogger<ParkingAreaController> logger

         ){

            _logger = logger;
            _parkingAreaClient = parkingAreaClient;
            
        }

    


        [HttpGet("{Id}")]
        public ParkingAreaInfo GetParkingAreaById(int Id)
        {
            _logger.LogInformation($"Getting parkingArea with id: {Id}");
            //TO DO AWAIT CLIENT TO COMPLETE THE OPERATION

            var parkingArea = _parkingAreaClient.GetParkingAreaById(Id);
            return new ParkingAreaInfo(parkingArea);

        }

        [HttpGet]
        public List<ParkingAreaInfo> GetParkingAreas()
        {
            _logger.LogInformation($"Getting parkingAreas ");
            //TO DO AWAIT CLIENT TO COMPLETE THE OPERATION

            var parkingAreas = _parkingAreaClient.GetParkingAreas();
            return parkingAreas.Select(x => new ParkingAreaInfo(x)).ToList();

        }

        [HttpPost]
        public async Task CreateParkingArea(ParkingAreaInfo parkingAreaInfo)
        {
            Console.WriteLine($"ParkingArea with made with parkingAreaId: {parkingAreaInfo.Id}");
            
            await _parkingAreaClient.CreateParkingArea(parkingAreaInfo.Convert());

        }

         [HttpPatch]
        public async Task UpdateParkingArea(ParkingAreaInfo parkingAreaInfo)
        {
            Console.WriteLine($"ParkingArea with Id: {parkingAreaInfo.Id} updating...");
            
            await _parkingAreaClient.UpdateParkingArea(parkingAreaInfo.Convert());

        }


         [HttpDelete("{Id}")]
        public async Task DeleteParkingAreaById(int Id)
        {
            Console.WriteLine($"ParkingArea with Id: {Id} deleting...");
            
            await _parkingAreaClient.DeleteParkingArea( new ParkingArea{Id=Id});

        }
    }
}
