#if ANDROID
using Android.Content;
using Android.Locations;
using Android.OS;
using Android;
using AndroidX.Core.Content;
using AndroidX.Core.App;
using System.Threading.Tasks;
using System;
public class AndroidLocationService : ILocationService
{
    public async Task<(double Latitude, double Longitude)> GetLocationAsync()
    {
        var context = Android.App.Application.Context;

        var locationManager =
            (LocationManager)context.GetSystemService(Context.LocationService);

        var location = locationManager.GetLastKnownLocation(LocationManager.GpsProvider);

        if (location == null)
            throw new Exception("Location non disponibile");

        return (location.Latitude, location.Longitude);
    }
}
#endif