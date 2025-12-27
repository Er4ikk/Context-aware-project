using System.Threading.Tasks;

public interface ILocationService
{
    Task<(double Latitude, double Longitude)> GetLocationAsync();
}
