namespace KuyumcuWebApi.Models;

public class ProductImage:BaseModel {
    public string Path { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
}