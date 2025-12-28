using System.Diagnostics;
using System.Text.Json;
using CommunityToolkit.Maui.Core;
using smar_parking_mobile.Models;

namespace smar_parking_mobile.Services;



public class ParkingAreaService
{
    HttpClient _httpClient;
    JsonSerializerOptions _serializerOptions;
    string BaseUrlDevelop = DeviceInfo.Platform == DevicePlatform.Android 
                            ? "http://10.0.2.2:5265" 
                            : "http://localhost:5265";
    string BaseUrl = "http://smartparking2.com";

    public ParkingAreaService()
    {
        _httpClient = HttClientService.Instance.GetHttpClient();
        _serializerOptions = HttClientService.Instance.GetJsonSerializerOptions();
    }

    public async Task<List<ParkingAreaInfo>> GetParkingAreas()
    {
        List<ParkingAreaInfo>? Items = new List<ParkingAreaInfo>();

        Uri uri = new Uri(string.Format(BaseUrlDevelop + "/ParkingArea/api/ParkingArea/GetParkingAreas", string.Empty));
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Items = JsonSerializer.Deserialize<List<ParkingAreaInfo>>(content, _serializerOptions);
            }
        }
        catch (Exception ex)
        {
            string text= "An error occurred while getting the ParkinAreas: " + ex.Message;
            await ToastService.ShowToast(text,ToastDuration.Short,14);
            Debug.WriteLine(text);
            Debug.Write("StackTrace: " +ex.StackTrace );
        }

        return Items;
    }
}