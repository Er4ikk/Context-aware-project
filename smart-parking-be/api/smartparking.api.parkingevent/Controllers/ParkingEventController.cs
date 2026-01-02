

using Microsoft.AspNetCore.Mvc;
using smartparking.db.parkingarea;
using smartparking.db.parkingevent;
using smartparking.db.postgres;
using NetTopologySuite.Geometries;
using System.Linq;

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

        [HttpGet("{Id}")]
        public List<ParkingEventInfo> GetParkingEvenstByParkingAreaId(int Id)
        {
            _logger.LogInformation($"Getting parkingEvents with area id: {Id}");
            //TO DO AWAIT CLIENT TO COMPLETE THE OPERATION

            List<ParkingEvent> parkingEvents = _parkingEventClient.GetParkingEventsByParkingAreaId(Id);
            List<ParkingEventInfo> parkingEventInfos = new List<ParkingEventInfo>();

            parkingEvents.ForEach((el) => parkingEventInfos.Add(new ParkingEventInfo(el)));
            return parkingEventInfos;

        }

        [HttpGet("{UserId}")]
        public List<ParkingEventInfo> GetParkingEvenstByUserId(int UserId)
        {
            _logger.LogInformation($"Getting parkingEvents with user id: {UserId}");
            //TO DO AWAIT CLIENT TO COMPLETE THE OPERATION

            List<ParkingEvent> parkingEvents = _parkingEventClient.GetParkingEventsByUserId(UserId);
            List<ParkingEventInfo> parkingEventInfos = new List<ParkingEventInfo>();

            parkingEvents.ForEach((el) => parkingEventInfos.Add(new ParkingEventInfo(el)));
            return parkingEventInfos;

        }


        [HttpGet("{start}/{end}/{parkingAreaId}")]
        public List<ParkingEventInfo> GetParkingEvenstByTimeRange(DateTimeOffset start, DateTimeOffset end, int parkingAreaId)
        {
            _logger.LogInformation($"Getting events between time range {start} - {end} parking area id: {parkingAreaId}");
            //TO DO AWAIT CLIENT TO COMPLETE THE OPERATION

            List<ParkingEvent> parkingEvents = _parkingEventClient.GetParkingEventsByTimeRange(start, end);
            List<ParkingEventInfo> parkingEventInfos = new List<ParkingEventInfo>();

            parkingEvents.ForEach((el) => parkingEventInfos.Add(new ParkingEventInfo(el)));
            return parkingEventInfos.Where(parkingEvent => parkingEvent.ParkingAreaId == parkingAreaId).ToList();

        }


        [HttpGet("{start}/{end}")]
        public List<ParkingAreaWrapper> GetParkingAreasSnapshotByTimeRange(DateTimeOffset start, DateTimeOffset end)
        {
            _logger.LogInformation($"Getting parking area snaphsot between time range {start} - {end}");
            //TO DO AWAIT CLIENT TO COMPLETE THE OPERATION

            List<ParkingArea> parkingAreasSnapshot = _parkingEventClient.GetParkingAreaSnapshotByTimeRange(start, end);
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


        [HttpGet]
        public List<Feature> GetParkingEventsFeatures()
        {
            _logger.LogInformation($"Getting parkingEvents ");
            //TO DO AWAIT CLIENT TO COMPLETE THE OPERATION

            var parkingEvents = _parkingEventClient.GetParkingEvents();
            List<Coordinates?> coordinates = parkingEvents
            .Select(x => new ParkingEventInfo(x))
            .Select(x => x.ParkingCoordinates)
            .Where(parkingCoordinates => parkingCoordinates?.x != null && parkingCoordinates?.y != null)
            .ToList();

            List<Feature> parkingEventsFeatures = new List<Feature>();
            Point point;
            Feature feature;
            Coordinates coordinate;

            coordinates.ForEach(coordinate =>
            {
                if (coordinate != null)
                {
                    point = new Point(new Coordinate(coordinate.x, coordinate.y));
                    coordinate = new Coordinates(coordinate.x, coordinate.y);
                    feature = new Feature("Feature", new Properties(), new Geometry(point.GeometryType, coordinate));
                    parkingEventsFeatures.Add(feature);
                }

            });

            return parkingEventsFeatures;

        }

        [HttpGet]
        public List<ParkingAreaWrapper>? ExtractParkingAreasFromParkingEvents()
        {
            _logger.LogInformation($"Getting parking Events ");
            //TO DO AWAIT CLIENT TO COMPLETE THE OPERATION

            MultiPoint multiPoint;
            List<Point?> groupedPoints;
            NetTopologySuite.Geometries.Geometry hullGeometry;
            ParkingArea? parkingArea = null;
            ParkingAreaWrapper? wrapper = null;
            List<ParkingAreaWrapper> polygonsArea = new List<ParkingAreaWrapper>();
            Type type;


            List<ParkingEvent> parkingEvents = _parkingEventClient.GetParkingEvents();
            var groupedAreas = parkingEvents.GroupBy(parkingEvent => parkingEvent.ParkingAreaId).ToDictionary(el => el.Key, el => el.ToList());

            foreach (int key in groupedAreas.Keys)
            {
                // foreach (ParkingEvent parkingEvent in groupedAreas[key])
                // {
                   
                // }
                var parkingAreaData = _parkingEventClient.GetParkingAreaById(key);

                 groupedPoints = [.. groupedAreas[key].Where(x => x.ParkingCoordinates != null).Select(x => x.ParkingCoordinates)];

                    multiPoint = new MultiPoint(groupedPoints.ToArray());
                    multiPoint.Normalize();
                    hullGeometry = multiPoint.ConvexHull();
                    type = hullGeometry.GetType();

                    if (type == typeof(Polygon))
                    {
                        parkingArea = new ParkingArea((Polygon)hullGeometry, parkingAreaData.MaxCapacity, parkingAreaData.PlacesLeft);
                        wrapper = new ParkingAreaWrapper(parkingArea);
                        polygonsArea.Add(wrapper);
                    }


            }

            return polygonsArea;

        }

        [HttpPost]
        public async Task CreateParkingEvent(ParkingEventInfo parkingEventInfo)
        {
            Console.WriteLine($"ParkingEvent with made with parkingEventId: {parkingEventInfo.Id}");

            await _parkingEventClient.CreateParkingEvent(parkingEventInfo.Convert());
            if (parkingEventInfo.EventType == EventType.LEAVING)
            {
                await _parkingEventClient.ReducePlaceAvailable(parkingEventInfo.ParkingAreaId);
            }
            else
            {
                await _parkingEventClient.AddPlaceAvailable(parkingEventInfo.ParkingAreaId);
            }


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
            _logger.LogInformation("Reducing place available to " + parkingAreaId + "...");
            await _parkingEventClient.ReducePlaceAvailable(parkingAreaId);

        }


        [HttpPatch]
        public async Task AddPlaceLeft(int parkingAreaId)
        {
            _logger.LogInformation("Add place available to " + parkingAreaId + "...");
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
