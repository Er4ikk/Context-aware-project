using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Mapsui;
using Mapsui.Extensions;
using Mapsui.Tiling;
using Mapsui.Widgets;
using Mapsui.Widgets.ButtonWidgets;
using Mapsui.Widgets.InfoWidgets;
using Mapsui.Widgets.ScaleBar;
using System;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using Avalonia.Threading;


namespace SmartParkingMobile.ViewModels;

public partial class MapViewModel : ViewModelBase
{
  public string Name => "OpenStreetMap";
  public string Category => "Basic";
  private readonly ILocationService _locationService;

  [ObservableProperty]
  private Mapsui.Map _map;

  public Task<Mapsui.Map> CreateMapAsync()
  {
    return Task.FromResult(CreateMap());
  }

  public static Mapsui.Map CreateMap()
  {
    var map = new Mapsui.Map
    {
      CRS = "EPSG:3857"
    };
    map.Layers.Add(OpenStreetMap.CreateTileLayer());
    map.Widgets.Add(new ScaleBarWidget(map) { TextAlignment = Alignment.Center, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Top });
    map.Widgets.Add(new ZoomInOutWidget { Margin = new MRect(20, 40) });
    map.Widgets.Add(new MouseCoordinatesWidget());
    return map;
  }

      public async Task UpdateUserLocation()
    {
        try
        {
            var (lat, lon) = await _locationService.GetLocationAsync();

            var point = Mapsui.Projections.SphericalMercator
                .FromLonLat(lon, lat);

            Dispatcher.UIThread.Post(() =>
            {
                Map.Navigator.CenterOn(point.x, point.y);
                Map.Navigator.ZoomTo(100);
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"GPS error: {ex.Message}");
        }
    }



  public async Task ShowAlert(string Title, string Message)
  {
    var box = MessageBoxManager.GetMessageBoxStandard(Title, Message, ButtonEnum.Ok);
    await box.ShowAsync();
  }
  public MapViewModel(ILocationService locationService)
  {
    _locationService = locationService;
    _map = CreateMap();
    Task.Run(async () =>
    {
      try
      {
        await UpdateUserLocation();
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Errore GPS: {ex.Message}");
      }
    });
  }

  public MapViewModel()
  {
     _map = CreateMap();
  }

}
