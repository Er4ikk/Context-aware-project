using CommunityToolkit.Mvvm.ComponentModel;

namespace SmartParkingMobile.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _greeting = "Hello";
    
    public HeaderViewModel Header { get; } = new HeaderViewModel();
    public MapViewModel Map {get;} = new MapViewModel();

    #if ANDROID
        DataContext = new MapViewModel(new AndroidLocationService());
       
    #endif

    

 
}
