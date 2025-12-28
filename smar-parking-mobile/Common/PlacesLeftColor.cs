namespace smar_parking_mobile.Common;

public static class PlacesLeftColor
{
    public static Color GetColorForPlacesLeft(int maxCapacity,int placesLeft)
    {   
        if (placesLeft < maxCapacity / 4)
        return Colors.Red;
      else if (placesLeft >= maxCapacity / 4 && placesLeft <= maxCapacity / 2)
        return Colors.Yellow;
      else if (placesLeft > maxCapacity / 2)
        return Colors.Green;
      else
        return Colors.Brown;
    } 
}