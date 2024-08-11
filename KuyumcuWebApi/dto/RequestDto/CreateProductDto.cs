namespace KuyumcuWebApi.dto;

public class CreateProductRequestDto {

    public string Name { get; set; }
    public string Description { get; set; }
    public string Category { get; set; } // Ürün kategorisi (yüzük, kolye, vb.)
    public int Stock { get; set; }
    public int Karat { get; set; }
    public decimal Price { get; set; } // Ürün fiyatı
    public decimal Weight { get; set; } // Ürün ağırlığı (gram cinsinden)
    public string Material { get; set; } // Ürünün yapıldığı materyal (altın, gümüş, vb.)
    public IFormFile[] Photos {get; set;} 
}