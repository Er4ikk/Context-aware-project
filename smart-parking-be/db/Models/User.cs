using System.ComponentModel.DataAnnotations.Schema;

public class User
{
    public User()
    {
        
    }

    public User(string mail,string pwd)
    {   
       
        this.Mail=mail;
        this.Password = pwd;

    }  

    [Column("id")]
    public int Id{get;set;}
    [Column("mail")]
    public string Mail {get;set;}
    [Column("pwd")]
    public string Password {get;set;}
}