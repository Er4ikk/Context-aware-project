
using smartparking.db.parkingarea;
using smartparking.db.parkingevent;


namespace smartparking.db.postgres
{


    public class PostGresClient(ILogger<PostGresClient> logger, IConfiguration configuration)
    {
        private readonly ILogger<PostGresClient> _logger = logger;
        private readonly ParkingAreaContext _ParkingAreaContext = new ParkingAreaContext(configuration);

        private readonly ParkingEventContext _ParkingEventContext = new(configuration);

        #region ParkingArea

        public List<ParkingArea> GetParkingAreas()
        {
            _logger.LogInformation("Getting parkingAreas");
            return _ParkingAreaContext.GetParkingAreas();
        }



        public ParkingArea GetParkingAreaById(int id)
        {
            _logger.LogInformation($"Getting information for parkingArea with id {id}");
            return _ParkingAreaContext.GetParkingAreaById(id);
        }


        public async Task CreateParkingArea(ParkingArea parkingArea)
        {
            _logger.LogInformation($"Creating ParkingArea: {parkingArea.Id}");
            await _ParkingAreaContext.CreateParkingArea(parkingArea);
        }


        public async Task UpdateParkingArea(ParkingArea parkingArea)
        {
            _logger.LogInformation($"Updating parkingArea with Id: {parkingArea.Id}");
            await _ParkingAreaContext.UpdateParkingArea(parkingArea);
        }


        public async Task DeleteParkingArea(ParkingArea parkingArea)
        {
            _logger.LogInformation($"Deleting ParkingArea with Id: {parkingArea.Id}");
            await _ParkingAreaContext.DeleteParkingArea(parkingArea);
        }


        public async Task DeleteParkingAreaById(int id)
        {
            _logger.LogInformation($"Deleting ParkingArea with Id: {id}");
            await _ParkingAreaContext.DeleteParkingAreaById(id);
        }

        #endregion
        
         #region ParkingEvent

        public List<ParkingEvent> GetParkingEvents()
        {
            _logger.LogInformation("Getting ParkingEvents");
            return _ParkingEventContext.GetParkingEvents();
        }

        public ParkingEvent GetParkingEventById(int id)
        {
            _logger.LogInformation($"Getting information for ParkingEvent with id {id}");
            return _ParkingEventContext.GetParkingEventById(id);
        }

     
        public async Task CreateParkingEvent(ParkingEvent ParkingEvent)
        {
            _logger.LogInformation($"Creating ParkingEvent with id: {ParkingEvent.Id}");
            await _ParkingEventContext.CreateParkingEvent(ParkingEvent);
        }


        public async Task UpdateParkingEvent(ParkingEvent ParkingEvent)
        {
            _logger.LogInformation($"Updating ParkingEvent with Id: {ParkingEvent.Id}");
            await _ParkingEventContext.UpdateParkingEvent(ParkingEvent);
        }


        public async Task DeleteParkingEvent(ParkingEvent ParkingEvent)
        {
            _logger.LogInformation($"Deleting ParkingEvent with Id: {ParkingEvent.Id}");
            await _ParkingEventContext.DeleteParkingEvent(ParkingEvent);
        }
        #endregion

   


        



    }
}
