using CommunityToolkit.Mvvm.Messaging.Messages;

public class ActivityMessage : ValueChangedMessage<string>
{
    public ActivityMessage(string value) : base(value)
    {
    }
}