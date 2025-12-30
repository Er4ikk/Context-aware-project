
using System.Collections.ObjectModel;
using smar_parking_mobile.Models;
using smar_parking_mobile.Services;
using smar_parking_mobile.Views;

namespace smar_parking_mobile.ViewModels;

public partial class UserViewModel : ViewModelBase
{

    private string _searchHint = "Search";
 
    private string[] ParkingAreas;

    private UserService _userService;
    private ParkingEventService _parkingEventService;
    public ObservableCollection<ParkingEventInfo> UserParkingEvents { get; set; } = new();
    public UserViewModel()
    {
 
       _userService= new UserService();
       _parkingEventService = new ParkingEventService();
    }


  public async Task Authenticate(string mail, string password)
  {  
    await _userService.Authenticate(mail,password);
  }

   public async Task LoadParkingEventsOfUser(int UserId)
  {
    var userParkingEvents = await _parkingEventService.GetParkingEventsByUserId(UserId);
    

   MainThread.BeginInvokeOnMainThread(() =>
    {
      UserParkingEvents.Clear();
      foreach (var parkingEvent in userParkingEvents)
      {
        UserParkingEvents.Add(parkingEvent);
      }
      
    });
  }
 
}
