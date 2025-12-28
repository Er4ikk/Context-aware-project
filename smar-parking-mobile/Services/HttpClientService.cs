using System.Text.Json;

namespace smar_parking_mobile.Services;

public sealed class HttClientService
{

    private static HttClientService instance = null;
    private static readonly HttpClient _client = new HttpClient();
    private static readonly JsonSerializerOptions _serializerOptions =new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

    HttClientService()
    {
    }


    public static  HttClientService Instance
    {
        get
        {
            lock (_client)
            {
                if (instance == null)
                {
                    instance = new HttClientService();
                }
                return instance;
            }
        }
    }

    public HttpClient GetHttpClient()
    {
        return _client;
    }

    public JsonSerializerOptions GetJsonSerializerOptions()
    {
        return _serializerOptions;
    }
}