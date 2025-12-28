using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace smar_parking_mobile.Services;

public static class ToastService
{

    static CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    public static async Task ShowToast(string text, ToastDuration duration, double fontSize)
    {


        var toast = Toast.Make(text, duration, fontSize);

        await toast.Show(cancellationTokenSource.Token);
    }
}