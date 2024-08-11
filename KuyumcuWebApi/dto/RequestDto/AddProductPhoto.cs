namespace KuyumcuWebApi.dto;

public class AddProductPhotoDto {
    public int ProductId { get; set; }
    public IFormFile Photo { get; set; }
}