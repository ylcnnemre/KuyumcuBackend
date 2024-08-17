namespace KuyumcuWebApi.dto;

public class UpdateOrderDto {
    public int OrderId { get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; }
}