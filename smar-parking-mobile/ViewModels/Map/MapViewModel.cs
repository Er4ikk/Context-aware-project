using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Maps;
using smar_parking_mobile.Common;
using smar_parking_mobile.Models;
using smar_parking_mobile.Services;

namespace smar_parking_mobile.ViewModels;

public partial class MapViewModel : BindableObject
{
  public string Name => "OpenStreetMap";
  public string Category => "Basic";
  private readonly ParkingAreaService _parkingAreaService;


  public ObservableCollection<ParkingAreaInfo> ParkingAreas { get; set; } = new();

  public MapViewModel()
  {
    //  _map = CreateMap();

    _parkingAreaService = new ParkingAreaService();
    Task.Run(async () => await LoadParkingAreas());
  }


  public async Task LoadParkingAreas()
  {
    var areas = await _parkingAreaService.GetParkingAreas();

    // Torniamo sul thread principale per aggiornare la UI
    // In Avalonia si usa Dispatcher.UIThread, in MAUI MainThread
    MainThread.BeginInvokeOnMainThread(() =>
    {
      ParkingAreas.Clear();
      foreach (var area in areas)
      {
        ParkingAreas.Add(area);
      }
    });
  }

  public async Task ZoomToUserLocation(Microsoft.Maui.Controls.Maps.Map map)
  {
    try
    {

      Location? location = await UserPosition.GetUserLocationAsync();

      if (location != null)
      {

        var mapSpan = MapSpan.FromCenterAndRadius(
            new Location(location.Latitude, location.Longitude),
            Distance.FromKilometers(0.5));

        MainThread.BeginInvokeOnMainThread(() =>
        {
          map.MoveToRegion(mapSpan);
        });
      }
    }
    catch (Exception ex)
    {
      string text="Error when Zooming to User location: " + ex.Message;
      await ToastService.ShowToast(text,ToastDuration.Long,14);

      Debug.WriteLine(text);
      Debug.WriteLine(ex.StackTrace);
    }
  }








}



internal class PopupOptions
{
  public bool CanBeDismissedByTappingOutsideOfPopup { get; set; }
  public RoundRectangle Shape { get; set; }
}