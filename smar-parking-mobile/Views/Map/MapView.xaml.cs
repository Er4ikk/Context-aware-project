


using Microsoft.Maui.Controls.Maps;
using smar_parking_mobile.Common;
using smar_parking_mobile.Models;
using smar_parking_mobile.ViewModels;

namespace smar_parking_mobile.Views;

public partial class MapView : ContentView
{
    public MapView()
    {
        InitializeComponent();
        var vm = Handler?.MauiContext?.Services.GetService<MapViewModel>()
                   ?? App.Current.Handler.MauiContext.Services.GetService<MapViewModel>();
        BindingContext = vm;

        if (vm?.ParkingAreas != null)
        {
            vm.ParkingAreas.CollectionChanged += (s, e) => UpdateMapPins(vm);

        }
    }

    protected override async void OnParentSet()
    {
        base.OnParentSet();
        if (Parent != null)
        {
            var vm = (MapViewModel)BindingContext;
            await vm.ZoomToUserLocation(map);
        }
    }

    private void UpdateMapPins(MapViewModel vm)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            map.MapElements.Clear();
            foreach (ParkingAreaInfo area in vm.ParkingAreas)
            {

                if (area.Area != null && area.Area.Length > 0)
                {
                    var color = PlacesLeftColor.GetColorForPlacesLeft(area.MaxCapacity, area.PlacesLeft);
                    var geometry = GeoJsonConverterMaui.ConvertGeoJsonToMauiLocations(area.Area);
                    var polygon = new Polygon
                    {
                        StrokeColor = color,
                        StrokeWidth = 2,
                        FillColor = color,
                    };

                    foreach (Location? loc in geometry)
                    {
                        polygon.Geopath.Add(loc);
                    }
                    map.MapElements.Add(polygon);

                    var center = CalculateCentroid(geometry);
                    map.Pins.Add(new Pin
                    {
                        Label = $"parking-area- {area.Id}",
                        Address = $"Places Left: {area.PlacesLeft}",
                        Location = center,
                        Type = PinType.Generic
                    });
                }


            }


        });
    }

    private Location CalculateCentroid(List<Location> nodes)
    {
        double lat = 0, lon = 0;
        foreach (var node in nodes)
        {
            lat += node.Latitude;
            lon += node.Longitude;
        }
        return new Location(lat / nodes.Count, lon / nodes.Count);
    }


}