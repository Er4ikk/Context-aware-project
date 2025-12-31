using Android.App;
using Android.Content.PM;
using Android.OS;

namespace smar_parking_mobile;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        StartActivityTracking();
    }
    
    private async void StartActivityTracking()
    {
        await Task.Delay(1000); 

        var status = await Permissions.RequestAsync<Permissions.Sensors>();
        var Positionstatus = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
        
        if (status == PermissionStatus.Granted && Positionstatus == PermissionStatus.Granted)
        {
            var helper = new ActivityRecognitionHelper();
            helper.StartTracking();
        }

        
    }
}
