using CommunityToolkit.Mvvm.ComponentModel;

namespace SmartParkingMobile.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _greeting = "Hello";
}
