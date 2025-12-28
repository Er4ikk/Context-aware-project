

namespace smar_parking_mobile.Views;

public partial class TabView : ContentView
{

    public string releaseSmartButtonText = "Release a Smart Bike";
    public TabView()
    {
        InitializeComponent();
    }

    private void ReleaseSmartBikeBtn_Clicked(object sender, EventArgs e)
    {
        Button releaseSmartButton = (Button)sender;
        if(releaseSmartButton.Text == "Release a Smart Bike")
        {
            releaseSmartButton.Text = "Confirm Parking";
        }
        else
        {
            releaseSmartButton.Text = "Release a Smart Bike";
        }
    }
}