using System.Text.Json.Serialization;

namespace smar_parking_mobile.Models;

public class UserInfo
{

    [JsonConstructor]
    public UserInfo(string mail, string password)
    {
        this.Mail = mail;
        this.Password = password;
    }



    // [JsonIgnore]

    // public int Id { get; set; }
    public string Mail { get; set; }

    public string Password { get; set; }
   

}