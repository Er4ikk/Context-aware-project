using Android.Gms.Location;
using Android.App;
using smar_parking_mobile.Services;
using CommunityToolkit.Maui.Core;
using System.Diagnostics;
using Android.Content;
using Android.OS;

public class ActivityRecognitionHelper
{
    public void StartTracking()
    {

        // var status = await Permissions.CheckStatusAsync<Permissions.Sensors>();
        // if (status != PermissionStatus.Granted)
        // {
        //     status = await Permissions.RequestAsync<Permissions.Sensors>();
        // }
        try
        {
            var context = Android.App.Application.Context;
            var client = ActivityRecognition.GetClient(context);

            var intent = new Intent(context, typeof(ActivityRecognitionBroadcastReceiver));

            var pendingIntent = PendingIntent.GetBroadcast(context, 0, intent,
                PendingIntentFlags.UpdateCurrent);

            var task = client.RequestActivityUpdatesAsync(5000, pendingIntent);
        
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Write(ex.Message);
            // await ToastService.ShowToast("Error encountered: " +ex.Message,ToastDuration.Long,14 );
        }


        // task.AddOnSuccessListener(new OnSuccessListener());
        // task.AddOnFailureListener(new OnFailureListener());

    }

 


}