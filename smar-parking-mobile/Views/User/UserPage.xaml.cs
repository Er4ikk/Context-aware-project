
using System.Diagnostics;
using CommunityToolkit.Maui.Core;
using smar_parking_mobile.Services;
using smar_parking_mobile.ViewModels;

namespace smar_parking_mobile.Views;

public partial class UserPage : ContentPage
{

    private UserViewModel viewModel;
    public UserPage()
    {
        InitializeComponent();
        var vm = Handler?.MauiContext?.Services.GetService<UserViewModel>()
                  ?? App.Current.Handler.MauiContext.Services.GetService<UserViewModel>();
        BindingContext = vm;
        viewModel = (UserViewModel)BindingContext;
    }


    private void LogIn(object sender, EventArgs e)
    {
        Debug.WriteLine("LOgin pressed");
    }

    private async void OnSubmitClicked(object sender, EventArgs e)
    {
        string mail = EmailEntry.Text;
        string password = PasswordEntry.Text;

        if (string.IsNullOrWhiteSpace(mail) || string.IsNullOrWhiteSpace(password))
        {
            await ToastService.ShowToast("Mail or Password are not inserted", ToastDuration.Short, 14);
            return;
        }
        else
        {
            try
            {
                await viewModel.Authenticate(mail,password);
                await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
            }
            catch (Exception ex)
            {
                await ToastService.ShowToast(ex.Message, ToastDuration.Long, 14);
            }
        }
        Debug.WriteLine("Login button pressed");
    }
}