using Android.Content;
using Android.Gms.Location;
using CommunityToolkit.Mvvm.Messaging;

[BroadcastReceiver(Enabled = true, Exported = true)]
public class ActivityRecognitionBroadcastReceiver : BroadcastReceiver
{
    public override void OnReceive(Context context, Intent intent)
    {
        if (ActivityRecognitionResult.HasResult(intent))
        {
            var result = ActivityRecognitionResult.ExtractResult(intent);
            var probableActivity = result.MostProbableActivity;

            string activityName = probableActivity.Type switch
            {
                DetectedActivity.Walking => "Walking",
                DetectedActivity.OnBicycle => "Biking",
                DetectedActivity.Running => "Running",
                _ => "Unknown"
            };
           

            int confidence = probableActivity.Confidence; 

            if (confidence > 75) 
            {
                WeakReferenceMessenger.Default.Send(new ActivityMessage(activityName));
                System.Diagnostics.Debug.WriteLine($"Activity received: {activityName} ({confidence}%)");
            }
        }
    }
}