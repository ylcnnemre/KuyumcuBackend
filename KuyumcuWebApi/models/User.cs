using System.ComponentModel.DataAnnotations;

namespace KuyumcuWebApi.Models;

public class User:BaseModel {
    public string FirstName { get; set;}
    public string LastName { get; set;}
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    public string Phone   { get; set; }
    public bool isActive { get; set; } = true;
    public int RoleId { get; set; }
    public Role role{ get; set; } = new Role();
    public Address? address {get;set;}
    public ICollection<Order> orders = new List<Order>();
}