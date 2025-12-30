using smar_parking_mobile.Views;

namespace smar_parking_mobile;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		//ROUTING
		Routing.RegisterRoute(nameof(UserPage), typeof(UserPage));
		Routing.RegisterRoute(nameof(UserLoggedPage), typeof(UserLoggedPage));
	}
}
