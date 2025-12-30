using System.Text.Json.Serialization;

namespace user.api.user;

public class UserInfo
{

    [JsonConstructor]
    public UserInfo(string mail, string password)
    {
        this.Mail = mail;
        this.Password = password;
    }


    public UserInfo(User user)
    {

        this.Mail = user.Mail;
        this.Password = user.Password;
        this.Id = user.Id;
    }

    // [JsonIgnore]

    public int Id { get; set; }
    public string Mail { get; set; }

    public string Password { get; set; }
    public User Convert()
    {
        User user = new User();
        user.Mail = this.Mail;
        user.Password = this.Password;
        // user.Id = this.Id;
        return user;
    }

}