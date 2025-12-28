using System.Threading.Tasks;

using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls.Shapes;

namespace smar_parking_mobile.ViewModels;

public partial class MapViewModel : ViewModelBase
{
  public string Name => "OpenStreetMap";
  public string Category => "Basic";

  // public Map Map;

  // public Task<Map> CreateMapAsync()
  // {
  //   return Task.FromResult(CreateMap());
  // }

  // public  Map CreateMap()
  // {
  //   // var map = new Mapsui.Map
  //   // {
  //   //   CRS = "EPSG:3857"
  //   // };
  //   // map.Layers.Add(OpenStreetMap.CreateTileLayer());
  //   // map.Widgets.Add(new ScaleBarWidget(map) { TextAlignment = Alignment.Center, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Top });
  //   // map.Widgets.Add(new ZoomInOutWidget { Margin = new MRect(20, 40) });
  //   // map.Widgets.Add(new MouseCoordinatesWidget());
  //   // return map;
  // }

      public async Task UpdateUserLocation()
    {
        // try
        // {
        //     var (lat, lon) = await _locationService.GetLocationAsync();

        //     var point = Mapsui.Projections.SphericalMercator
        //         .FromLonLat(lon, lat);

        //     Dispatcher.UIThread.Post(() =>
        //     {
        //         Map.Navigator.CenterOn(point.x, point.y);
        //         Map.Navigator.ZoomTo(100);
        //     });
        // }
        // catch (Exception ex)
        // {
        //     Console.WriteLine($"GPS error: {ex.Message}");
        // }
    }



  // public async Task ShowAlert(string Title, string Message)
  // {
  //   await this.ShowPopupAsync(new Label
  //       {
  //           Text = "This is a very important message!"
  //       }, new PopupOptions
  //       {
  //           CanBeDismissedByTappingOutsideOfPopup = false,
  //           Shape = new RoundRectangle
  //           {
  //               CornerRadius = new CornerRadius(20, 20, 20, 20),
  //               StrokeThickness = 2,
  //               Stroke = Colors.LightGray
  //           }
  //       });

  // }
  // public MapViewModel(ILocationService locationService)
  // {
  //   _locationService = locationService;
  //   _map = CreateMap();
  //   Task.Run(async () =>
  //   {
  //     try
  //     {
  //       await UpdateUserLocation();
  //     }
  //     catch (Exception ex)
  //     {
  //       Console.WriteLine($"Errore GPS: {ex.Message}");
  //     }
  //   });
  // }

  public MapViewModel()
  {
    //  _map = CreateMap();
  }

}

internal class PopupOptions
{
    public bool CanBeDismissedByTappingOutsideOfPopup { get; set; }
    public RoundRectangle Shape { get; set; }
}