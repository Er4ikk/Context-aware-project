

using Microsoft.AspNetCore.Mvc;
using smartparking.db.parkingarea;
using smartparking.db.parkingevent;
using smartparking.db.postgres;
using NetTopologySuite.IO;

namespace parkingEvent.api.parkingEvent
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ParkingEventController : ControllerBase
    {

        private readonly PostGresClient _parkingEventClient;
        private readonly ILogger<ParkingEventController> _logger;
        private GeoJsonWriter geoJsonWriter = new GeoJsonWriter();
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
        
         [HttpGet("{Id}")]
        public List<ParkingEventInfo> GetParkingEvenstByParkingAreaId(int Id)
        {
            _logger.LogInformation($"Getting parkingEvents with area id: {Id}");
            //TO DO AWAIT CLIENT TO COMPLETE THE OPERATION

            List<ParkingEvent> parkingEvents = _parkingEventClient.GetParkingEventsByParkingAreaId(Id);
            List<ParkingEventInfo> parkingEventInfos= new List<ParkingEventInfo>();

            parkingEvents.ForEach( (el ) => parkingEventInfos.Add(new ParkingEventInfo(el)));
            return parkingEventInfos;

        }

        [HttpGet("{start}/{end}")]
        public List<ParkingEventInfo> GetParkingEvenstByTimeRange(DateTimeOffset start,DateTimeOffset end)
        {
            _logger.LogInformation($"Getting events between time range {start} - {end}");
            //TO DO AWAIT CLIENT TO COMPLETE THE OPERATION

            List<ParkingEvent> parkingEvents = _parkingEventClient.GetParkingEventsByTimeRange(start,end);
            List<ParkingEventInfo> parkingEventInfos= new List<ParkingEventInfo>();

            parkingEvents.ForEach( (el ) => parkingEventInfos.Add(new ParkingEventInfo(el)));
            return parkingEventInfos;

        }


        [HttpGet("{start}/{end}")]
        public List<ParkingAreaWrapper> GetParkingAreasSnapshotByTimeRange(DateTimeOffset start,DateTimeOffset end)
        {
            _logger.LogInformation($"Getting parking area snaphsot between time range {start} - {end}");
            //TO DO AWAIT CLIENT TO COMPLETE THE OPERATION

            List<ParkingArea> parkingAreasSnapshot = _parkingEventClient.GetParkingAreaSnapshotByTimeRange(start,end);
            List<ParkingAreaWrapper> parkingAreaWrappers = parkingAreasSnapshot.Select(snapshot => new ParkingAreaWrapper(snapshot)).ToList();
            
            return parkingAreaWrappers;

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

        [HttpPatch]
        public async Task ReducePlaceLeft(int parkingAreaId)
        {
            _logger.LogInformation("Reducing place available to " + parkingAreaId +"...");
            await _parkingEventClient.ReducePlaceAvailable(parkingAreaId);
            
        }


        [HttpPatch]
        public async Task AddPlaceLeft(int parkingAreaId)
        {
            _logger.LogInformation("Add place available to " + parkingAreaId +"...");
            await _parkingEventClient.AddPlaceAvailable(parkingAreaId);
            
        }


        [HttpDelete("{Id}")]
        public async Task DeleteParkingEventById(int Id)
        {
            Console.WriteLine($"ParkingEvent with Id: {Id} deleting...");

            await _parkingEventClient.DeleteParkingEvent(new ParkingEvent { Id = Id });

        }
    }


}
