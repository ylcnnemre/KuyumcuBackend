namespace KuyumcuWebApi.Service;

public class FileService
{
    private readonly string _imageFolderPath;

    public FileService(IWebHostEnvironment environment)
    {
        _imageFolderPath = Path.Combine(environment.WebRootPath, "images");
        if (!Directory.Exists(_imageFolderPath))
        {
            Directory.CreateDirectory(_imageFolderPath); // Klasör yoksa oluştur
        }
    }

    public async Task<string> SavePhotoAsync(IFormFile photo)
    {
        if (photo == null || photo.Length == 0)
            throw new ArgumentException("Invalid photo file.");

        // Dosya adını ve uzantısını al
        var fileExtension = Path.GetExtension(photo.FileName);
        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(photo.FileName);

        string fileName;
        string filePath;

        do
        {
            // Benzersiz bir ID oluştur (8 karakter uzunluğunda)
            var uniqueId = GenerateUniqueId(8);
            fileName = $"{fileNameWithoutExtension}_{uniqueId}{fileExtension}";
            filePath = Path.Combine(_imageFolderPath, fileName);

        } while (File.Exists(filePath)); // Dosya mevcutsa yeniden ID üret

        // Resmi dizine kaydet
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await photo.CopyToAsync(stream);
        }

        // Dosya URL'sini oluştur
        return fileName;
    }

    public async Task DeletePhotoAsync(string fileName)
    {
        var filePath = Path.Combine(_imageFolderPath, fileName);

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            await Task.CompletedTask; // İsterseniz burada başka işlemler yapabilirsiniz
        }
        else
        {
            throw new FileNotFoundException("Fotoğraf bulunamadı.", fileName);
        }
    }

    private string GenerateUniqueId(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}