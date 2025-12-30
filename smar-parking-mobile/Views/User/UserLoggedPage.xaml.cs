
using System.Diagnostics;
using CommunityToolkit.Maui.Core;
using smar_parking_mobile.Models;
using smar_parking_mobile.Services;
using smar_parking_mobile.ViewModels;

namespace smar_parking_mobile.Views;

public partial class UserLoggedPage : ContentPage
{

   public UserInfo CurrentUser { get; set; } = UserAuthentication.userInfo;
    private UserViewModel viewModel;
    public UserLoggedPage()
    {
        InitializeComponent();
        var vm = Handler?.MauiContext?.Services.GetService<UserViewModel>()
                  ?? App.Current.Handler.MauiContext.Services.GetService<UserViewModel>();
        BindingContext = vm;
        viewModel = (UserViewModel)BindingContext;
        CurrentUser = UserAuthentication.userInfo;
    }



   

    protected override async void OnAppearing()
{
    base.OnAppearing();
    
    if (UserAuthentication.userInfo != null)
    {
        var viewModel = (UserViewModel)this.BindingContext;
        await viewModel.LoadParkingEventsOfUser(UserAuthentication.userInfo.Id);
    }
}
}