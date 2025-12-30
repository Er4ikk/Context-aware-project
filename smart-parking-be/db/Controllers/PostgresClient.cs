
using smartparking.db.parkingarea;
using smartparking.db.parkingevent;


namespace smartparking.db.postgres
{


    public class PostGresClient(ILogger<PostGresClient> logger, IConfiguration configuration)
    {
        private readonly ILogger<PostGresClient> _logger = logger;
        private readonly ParkingAreaContext _ParkingAreaContext = new ParkingAreaContext(configuration);

        private readonly ParkingEventContext _ParkingEventContext = new(configuration);
        private readonly UserContext _UserContext= new UserContext(configuration);

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

        public async Task ReducePlaceAvailable(int parkingAreaId)
        {
            _logger.LogInformation($"Reducing Places Left ParkingArea with Id: {parkingAreaId}");
            await _ParkingAreaContext.ReducePlaceAvailable(parkingAreaId);
        }

        public async Task AddPlaceAvailable(int parkingAreaId)
        {
            _logger.LogInformation($"Adding Places Left ParkingArea with Id: {parkingAreaId}");
            await _ParkingAreaContext.AddPlaceAvailable(parkingAreaId);
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

        public List<ParkingEvent> GetParkingEventsByParkingAreaId(int id)
        {
            _logger.LogInformation($"Getting parking events for Parking Area Id {id}");
            return _ParkingEventContext.GetParkingEventsByParkingAreaId(id);
        }

        public List<ParkingEvent> GetParkingEventsByUserId(int UserId)
        {
            _logger.LogInformation($"Getting parking events by User Id {UserId}");
            return _ParkingEventContext.GetParkingEventsByUserId(UserId);
        }

        public List<ParkingEvent> GetParkingEventsByTimeRange(DateTimeOffset start, DateTimeOffset end)
        {
            _logger.LogInformation($"Getting parking events between time range {start} - {end}");
            return _ParkingEventContext.GetParkingEventsByTimeRange(start,end);
        }

        public List<ParkingArea> GetParkingAreaSnapshotByTimeRange(DateTimeOffset start, DateTimeOffset end)
        {
            _logger.LogInformation($"Getting Parking Area snapshots using from {start} - {end}");
            List<ParkingEvent> parkingEvents = GetParkingEventsByTimeRange(start,end);
            List<ParkingArea> parkingAreas = GetParkingAreas();
            List<ParkingEvent> parkingEventsForArea;

            parkingAreas.ForEach(parkingArea =>
            {
                parkingEventsForArea = parkingEvents.Where(ev => ev.ParkingAreaId == parkingArea.Id).ToList();

               parkingEventsForArea.ForEach(ev =>
               {
                   if (ev.EventType == EventType.PARKING && parkingArea.PlacesLeft < parkingArea.MaxCapacity)
                   {
                       parkingArea.PlacesLeft++;
                   }
                   else if (ev.EventType == EventType.LEAVING && parkingArea.PlacesLeft > 0)
                   {
                       parkingArea.PlacesLeft--;
                   }
               });
            });

            return parkingAreas;

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

        #region User
         public List<User> GetUsers()
        {
            _logger.LogInformation("Getting Users");
            return _UserContext.GetUsers();
        }



        public User GetUserById(int id)
        {
            _logger.LogInformation($"Getting information for User with id {id}");
            return _UserContext.GetUserById(id);
        }

        public User GetUserByMail(string mail)
        {
            _logger.LogInformation($"Getting information for User with mail {mail}");
            return _UserContext.GetUserByEmail(mail);
        }


        public async Task CreateUser(User User)
        {
            _logger.LogInformation($"Creating User: {User.Id}");
            await _UserContext.CreateUser(User);
        }


        public async Task UpdateUser(User User)
        {
            _logger.LogInformation($"Updating User with Id: {User.Id}");
            await _UserContext.UpdateUser(User);
        }


        public async Task DeleteUser(User User)
        {
            _logger.LogInformation($"Deleting User with Id: {User.Id}");
            await _UserContext.DeleteUser(User);
        }


        public async Task DeleteUserById(int id)
        {
            _logger.LogInformation($"Deleting User with Id: {id}");
            await _UserContext.DeleteUserById(id);
        }

         public async Task DeleteUserByMail(string mail)
        {
            _logger.LogInformation($"Deleting User with mail: {mail}");
            await _UserContext.DeleteUserByEmail(mail);
        }

  
        #endregion






    }
}
