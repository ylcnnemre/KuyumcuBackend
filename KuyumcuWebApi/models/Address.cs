namespace KuyumcuWebApi.Models;

public class Address : BaseModel {
    public string City {get;set;}
    public string District {get;set;}
    public string Description {get;set;}
    public int UserId {get;set;}
    public User User {get;set;}
}