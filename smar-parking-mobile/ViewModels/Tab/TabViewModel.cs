
using System.Diagnostics;
using CommunityToolkit.Maui.Core;
using smar_parking_mobile.Common;
using smar_parking_mobile.Models;
using smar_parking_mobile.Services;
using smar_parking_mobile.Views;

namespace smar_parking_mobile.ViewModels;

public partial class TabViewModel : ViewModelBase
{
    private string UserLabel = "User";
    private readonly ParkingEventService _parkingEventService;
    private readonly UserService _userService;



    public TabViewModel()
    {

        _parkingEventService = new ParkingEventService();
        _userService = new UserService();
    }


    public async Task ParkBike()
    {
        if (UserAuthentication.userInfo != null)
        {
            Location? location = await UserPosition.GetUserLocationAsync();
            ParkingEventInfo parkingEvent = new ParkingEventInfo();



            if (location != null)
            {

                Coordinates? userCoordinates = new Coordinates(location.Longitude, location.Latitude);
               
                bool isInsideParkingArea = CheckIfIsInsideAnyParkingArea(userCoordinates);

                if (isInsideParkingArea)
                {
                    parkingEvent.ParkingAreaId = ParkingAreaCache.parkingAreaRef.Id;
                    parkingEvent.UserId = UserAuthentication.userInfo.Id;
                    parkingEvent.EventType = EventType.PARKING;
                    parkingEvent.ParkingCoordinates = new Coordinates(location.Longitude, location.Latitude);
                    await _parkingEventService.CreateParkingEventAsync(parkingEvent);
                    Debug.WriteLine("Parking done with success");
                }
                else
                {
                    string text = "User Is not in any Parking area. cannot park the bicycle";
                    throw new Exception(text);
                }



            }
            else
            {
                string text = "Cannot get Location from User. cannot park the bycicle";
                throw new Exception(text);
            }


        }
        else
        {
            string text = "User is not Authenticated cannot park the bycicle";
            throw new Exception(text);
        }
    }


    public async Task ReleaseBike()
    {
        if (UserAuthentication.userInfo != null)
        {
             Location? location = await UserPosition.GetUserLocationAsync();
            ParkingEventInfo parkingEvent = new ParkingEventInfo();



            if (location != null)
            {

                Coordinates? userCoordinates = new Coordinates(location.Longitude, location.Latitude);
                //TO DO CALCULATE THE POSITION IN THE AREA
                bool isInsideParkingArea = CheckIfIsInsideAnyParkingArea(userCoordinates);

                if (isInsideParkingArea)
                {
                    parkingEvent.ParkingAreaId = ParkingAreaCache.parkingAreaRef.Id;
                    parkingEvent.UserId = UserAuthentication.userInfo.Id;
                    parkingEvent.EventType = EventType.LEAVING;
                    parkingEvent.ParkingCoordinates = new Coordinates(location.Longitude, location.Latitude);
                    await _parkingEventService.CreateParkingEventAsync(parkingEvent);
                    Debug.WriteLine("Parking done with success");
                }
                else
                {
                    string text = "User Is not in any Parking area. cannot release the bicycle";
                    throw new Exception(text);
                }
            }
            else
            {
                string text = "Cannot get Location from User. cannot park the bycicle";
                throw new Exception(text);
            }


        }
        else
        {
            string text = "User is not Authenticated cannot release the bycicle";
            throw new Exception(text);
        }
    }


    public bool CheckIfIsInsideAnyParkingArea(Coordinates? userCoordinates)
    {
        if (ParkingAreaCache.parkingAreas != null && userCoordinates != null)
        {
            foreach (ParkingAreaInfo area in ParkingAreaCache.parkingAreas)
            {
                if (_parkingEventService.isInsideParkingArea(area, userCoordinates))
                {
                    ParkingAreaCache.parkingAreaRef = area;
                    return true;
                }
            }
        }
        else
        {
            Debug.WriteLine($"Cannot find parkng areas or user coordinates are null {userCoordinates}");
            return false;
        }
        return false;

    }

}
