
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.Messaging;
using smar_parking_mobile.Services;

namespace smar_parking_mobile;

public partial class MainPage : ContentPage
{

	public MainPage()
	{
		InitializeComponent();
		
		WeakReferenceMessenger.Default.Register<ActivityMessage>(this, (r, m) =>
        {
           
            OnActivityDetected(m.Value);
        });
	}

	private void OnActivityDetected(string activity)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
           await ToastService.ShowToast($"The user is: {activity}",ToastDuration.Short,14 );
        });
    }

	
}
