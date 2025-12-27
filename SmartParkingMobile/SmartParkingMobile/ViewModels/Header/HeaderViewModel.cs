using CommunityToolkit.Mvvm.ComponentModel;
using SmartParkingMobile.Views;

namespace SmartParkingMobile.ViewModels;

public partial class HeaderViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _searchHint = "Search";
    [ObservableProperty]
    private string[] _parkingAreas;
    public HeaderViewModel()
    {
        // TO DO: CALL parking area API
        ParkingAreas =
        [
          "parking-area-1",
          "parking-area-3"  
        ];
       
    }
 
}
