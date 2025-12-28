
using smar_parking_mobile.Views;

namespace smar_parking_mobile.ViewModels;

public partial class UserViewModel : ViewModelBase
{

    private string _searchHint = "Search";
 
    private string[] ParkingAreas;
    public UserViewModel()
    {
        // TO DO: CALL parking area API
        ParkingAreas =
        [
          "parking-area-1",
          "parking-area-3"  
        ];
       
    }
 
}
