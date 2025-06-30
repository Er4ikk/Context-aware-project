

using Microsoft.AspNetCore.Mvc;
using smartparking.db.parkingevent;
using smartparking.db.postgres;

namespace parkingEvent.api.parkingEvent
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ParkingEventController : ControllerBase
    {
        
           private readonly PostGresClient _parkingEventClient;
        private readonly ILogger<ParkingEventController> _logger;
public ParkingEventController(
                            PostGresClient parkingEventClient,
                            ILogger<ParkingEventController> logger

                         )
        {

            _logger = logger;
            _parkingEventClient = parkingEventClient;

        }

    


        [HttpGet("{Id}")]
        public ParkingEventInfo GetParkingEventById(int Id)
        {
            _logger.LogInformation($"Getting parkingEvent with id: {Id}");
            //TO DO AWAIT CLIENT TO COMPLETE THE OPERATION

            var parkingEvent = _parkingEventClient.GetParkingEventById(Id);
            return new ParkingEventInfo(parkingEvent);

        }

        [HttpGet]
        public List<ParkingEventInfo> GetParkingEvents()
        {
            _logger.LogInformation($"Getting parkingEvents ");
            //TO DO AWAIT CLIENT TO COMPLETE THE OPERATION

            var parkingEvents = _parkingEventClient.GetParkingEvents();
            return parkingEvents.Select(x => new ParkingEventInfo(x)).ToList();

        }

        [HttpPost]
        public async Task CreateParkingEvent(ParkingEventInfo parkingEventInfo)
        {
            Console.WriteLine($"ParkingEvent with made with parkingEventId: {parkingEventInfo.Id}");
            
            await _parkingEventClient.CreateParkingEvent(parkingEventInfo.Convert());

        }

         [HttpPatch]
        public async Task UpdateParkingEvent(ParkingEventInfo parkingEventInfo)
        {
            Console.WriteLine($"ParkingEvent with Id: {parkingEventInfo.Id} updating...");
            
            await _parkingEventClient.UpdateParkingEvent(parkingEventInfo.Convert());

        }


         [HttpDelete("{Id}")]
        public async Task DeleteParkingEventById(int Id)
        {
            Console.WriteLine($"ParkingEvent with Id: {Id} deleting...");
            
            await _parkingEventClient.DeleteParkingEvent( new ParkingEvent{Id=Id});

        }
    }
}
