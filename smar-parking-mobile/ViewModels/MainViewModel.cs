
namespace smar_parking_mobile.ViewModels;

public partial class MainViewModel : ViewModelBase
{

    private string _greeting = "Hello";
    
    public HeaderViewModel Header { get; } = new HeaderViewModel();
    public MapViewModel Map {get;} = new MapViewModel();


    

 
}
