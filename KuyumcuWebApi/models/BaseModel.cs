namespace KuyumcuWebApi.Models;

public class BaseModel
{
    public int Id { get; set; }
    public bool IsDeleted { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}