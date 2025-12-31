

using System.Threading.Tasks;
using CommunityToolkit.Maui.Core;
using smar_parking_mobile.Services;
using smar_parking_mobile.ViewModels;

namespace smar_parking_mobile.Views;

public partial class TabView : ContentView
{
    private TabViewModel viewModel;

    public string AccountImageSource = "user.png";
    public string releaseSmartButtonText = "Release a Smart Bike";
    public TabView()
    {
        InitializeComponent();
        var vm = Handler?.MauiContext?.Services.GetService<TabViewModel>()
                  ?? App.Current.Handler.MauiContext.Services.GetService<TabViewModel>();
        BindingContext = vm;
        viewModel = (TabViewModel)BindingContext;

        
    }



    private async void ReleaseSmartBikeBtn_Clicked(object sender, EventArgs e)
    {
        Button releaseSmartButton = (Button)sender;
        if (releaseSmartButton.Text == "Release a SmartBike!")
        {
            try
            {
                await viewModel.ReleaseBike();
                releaseSmartButton.Text = "Confirm Parking";
                await ToastService.ShowToast("Bike succesfully released", ToastDuration.Long, 14);

            }
            catch (Exception ex)
            {
                await ToastService.ShowToast(ex.Message, ToastDuration.Long, 14);
            }

        }
        else
        {

            try
            {
                await viewModel.ParkBike();
                releaseSmartButton.Text = "Release a SmartBike!";
                await ToastService.ShowToast("Bike successfully parked", ToastDuration.Long, 14);
            }
            catch (Exception ex)
            {
                await ToastService.ShowToast(ex.Message, ToastDuration.Long, 14);
            }

        }
    }

    private async void GoToUserPage_Clicked(object sender, EventArgs e)
    {
        if (UserAuthentication.userInfo != null)
            await Shell.Current.GoToAsync(nameof(UserLoggedPage));
        else
            await Shell.Current.GoToAsync(nameof(UserPage));
    }


}