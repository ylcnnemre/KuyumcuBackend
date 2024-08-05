using System.ComponentModel.DataAnnotations;

namespace KuyumcuWebApi.Models;

public class Role {
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; }

    public ICollection<User> users{ get; set; } = new List<User>();

}