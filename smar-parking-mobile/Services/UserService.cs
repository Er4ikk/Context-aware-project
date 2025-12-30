using System.Diagnostics;
using System.Text.Json;
using CommunityToolkit.Maui.Core;
using smar_parking_mobile.Models;

namespace smar_parking_mobile.Services;


public class UserService
{
    HttpClient _httpClient;
    JsonSerializerOptions _serializerOptions;
    string BaseUrlDevelop = DeviceInfo.Platform == DevicePlatform.Android 
                            ? "http://10.0.2.2:5266" 
                            : "http://localhost:5266";
    string BaseUrl = "http://smartparking2.com";

    public UserService()
    {
        _httpClient = HttClientService.Instance.GetHttpClient();
        _serializerOptions = HttClientService.Instance.GetJsonSerializerOptions();
    }

    public async Task Authenticate(string mail, string pwd)
    {
        if (UserAuthentication.userInfo == null)
        {
            UserInfo? Item;

            Uri uri = new Uri(string.Format(BaseUrlDevelop + "/User/api/User/GetUserByMail/" + mail, string.Empty));
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Item = JsonSerializer.Deserialize<UserInfo>(content, _serializerOptions);

                    if (Item != null && Item.Password.Equals(pwd))
                    {
                        UserAuthentication.userInfo = Item;
                        Debug.WriteLine($"Authentication successfull for user with mail:{mail}");
                    }
                    else
                    {
                        throw new Exception($"Invalid password for user:{mail} or user not found");
                    }
                }
                else
                {
                    throw new Exception($"The response of the server doesn't indicate success: {response.StatusCode}. Content: {response.Content}");
                }
            }
            catch (Exception ex)
            {

                string text = "An error occurred while authentication: " + ex.Message;
                await ToastService.ShowToast(text, ToastDuration.Short, 14);
                Debug.WriteLine(text);
                Debug.Write("StackTrace: " + ex.StackTrace);
            }

        }
    }

    public void LogOut()
    {
        UserAuthentication.userInfo = null;
    }


}