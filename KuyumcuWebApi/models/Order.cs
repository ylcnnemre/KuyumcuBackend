namespace KuyumcuWebApi.Models;

public class Order:BaseModel {
    public int Quantity {get;set;}
    public string Description {get;set;}
    public bool Status {get;set;} = false;
    public Product product {get;set;}
    public int UserId {get;set;}
    public User User  {get;set;}

}