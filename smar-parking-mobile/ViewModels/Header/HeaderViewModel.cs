
using smar_parking_mobile.Views;

namespace smar_parking_mobile.ViewModels;

public partial class HeaderViewModel : ViewModelBase
{
    
    private string SearchHint = "Search";

    private string[] ParkingAreas;
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
