using System.Diagnostics;
using System.Text;
using System.Text.Json;
using CommunityToolkit.Maui.Core;
using GeoJSON.Net.Geometry;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using smar_parking_mobile.Models;

namespace smar_parking_mobile.Services;



public class ParkingEventService
{
    HttpClient _httpClient;
    JsonSerializerOptions _serializerOptions;

    GeoJsonReader geoJsonReader = new GeoJsonReader();
    GeoJsonWriter geoJsonWriter = new GeoJsonWriter();
    string BaseUrlDevelop = DeviceInfo.Platform == DevicePlatform.Android
                            ? "http://10.0.2.2"
                            : "http://localhost:5264";
    string BaseUrl = "http://smartparking2.com";

    public ParkingEventService()
    {
        _httpClient = HttClientService.Instance.GetHttpClient();
        _serializerOptions = HttClientService.Instance.GetJsonSerializerOptions();
         _httpClient.DefaultRequestHeaders.Host = "smartparking2.com";
    }

    public async Task<List<ParkingEventInfo>?> GetParkingEventsByUserId(int userId)
    {
        List<ParkingEventInfo>? Items = new List<ParkingEventInfo>();

        Uri uri = new Uri(string.Format(BaseUrlDevelop + "/ParkingEvent/api/ParkingEvent/GetParkingEvenstByUserId/" + userId, string.Empty));
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Items = JsonSerializer.Deserialize<List<ParkingEventInfo>>(content, _serializerOptions);
            }
        }
        catch (Exception ex)
        {

            string text = "An error occurred while getting the ParkinAreas: " + ex.Message;
            await ToastService.ShowToast(text, ToastDuration.Short, 14);
            Debug.WriteLine(text);
            Debug.Write("StackTrace: " + ex.StackTrace);
        }

        return Items;
    }


    public async Task CreateParkingEventAsync(ParkingEventInfo item)
    {
        Uri uri = new Uri(string.Format(BaseUrlDevelop + "/ParkingEvent/api/ParkingEvent/CreateParkingEvent", string.Empty));


        try
        {
            string json = JsonSerializer.Serialize<ParkingEventInfo>(item, _serializerOptions);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;

            response = await _httpClient.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
                Debug.WriteLine(@"\tParkingEventInfo successfully saved.");
        }
        catch (Exception ex)
        {

            string text = "An error occurred while creating Parking event: " + ex.Message;
            await ToastService.ShowToast(text, ToastDuration.Short, 14);
            Debug.WriteLine(text);
            Debug.Write("StackTrace: " + ex.StackTrace);
        }
    }


// https://github.com/NetTopologySuite/NetTopologySuite/issues/264
    public bool isInsideParkingArea(ParkingAreaInfo parkingAreaInfo, Models.Coordinates coordinates)
    {
        var geometry = geoJsonReader.Read<Geometry>(parkingAreaInfo.Area);
        var geoFactory = new NetTopologySuite.Geometries.Prepared.PreparedGeometryFactory();
        var preparedGeometry = geoFactory.Create(
              geometry
        );

        var isInside = preparedGeometry.Contains(Geometry.DefaultFactory.CreatePoint(new Coordinate(coordinates.x, coordinates.y)));
        return isInside;
    }

}