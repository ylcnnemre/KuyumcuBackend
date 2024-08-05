using System.ComponentModel.DataAnnotations;

namespace KuyumcuWebApi.Models;

public class User {
    public int Id { get; set;}
    public string FirstName { get; set;}
    public string LastName { get; set;}

    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    public int RoleId { get; set; }
    public Role role{ get; set; } = new Role();
}