namespace KuyumcuWebApi.dto;

public class CreateOrderRequestDto{
    public int UserId {get;set;}
    public int ProductId {get;set;}
    public int Quantity {get;set;}
    public string Description {get;set;}
}