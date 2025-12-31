using smar_parking_mobile.Models;

public static class UserAuthentication
{
    private static UserInfo _userInfo;
   public static event EventHandler UserChanged;

    public static UserInfo userInfo 
    {
        get => _userInfo;
        set 
        {
            if (_userInfo != value)
            {
                _userInfo = value;
                UserChanged?.Invoke(null, EventArgs.Empty);
            }
        }
    }
}