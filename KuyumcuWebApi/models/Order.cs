namespace KuyumcuWebApi.Models;

public class Order:BaseModel {
    public int Quantity {get;set;}
    public string Description {get;set;}

    public int ProductId {get;set;}
    public Product product {get;set;}
    public int UserId {get;set;}
    public User User  {get;set;}
    public int OrderStatusId {get;set;}
    public OrderStatus orderStatus {get;set;}

}