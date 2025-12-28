

namespace smar_parking_mobile.Common;

public static class UserPosition
{
    public static async Task<Location?> GetUserLocationAsync()
    {
        Location? location = await Geolocation.Default.GetLocationAsync(new GeolocationRequest
            {
                DesiredAccuracy = GeolocationAccuracy.Medium,
                Timeout = TimeSpan.FromSeconds(30)
            });

        return location;
    }
}