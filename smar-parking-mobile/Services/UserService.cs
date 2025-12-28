using System.Diagnostics;
using System.Text.Json;
using smar_parking_mobile.Models;

namespace smar_parking_mobile.Services;


public class UserService
{
    HttpClient _httpClient;
    JsonSerializerOptions _serializerOptions;
    string BaseUrlDevelop ="http://localhost:5266";
    string BaseUrl="http://smartparking2.com";

    public UserService()
    {
        _httpClient = HttClientService.Instance.GetHttpClient();
        _serializerOptions = HttClientService.Instance.GetJsonSerializerOptions();
    }

  
}