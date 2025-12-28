
using System.Diagnostics;
using Android.App.AppSearch;
using Android.Widget;

namespace smar_parking_mobile.Views;

public partial class HeaderView : ContentView
{

   public HeaderView()
    {
        InitializeComponent();
    }

    void OnTextChanged(object sender, EventArgs e)
    {
        SearchBar searchBar = (SearchBar)sender;   
        Debug.WriteLine(searchBar.Text);
        // searchResults.ItemsSource = new ListView
    }

  
}