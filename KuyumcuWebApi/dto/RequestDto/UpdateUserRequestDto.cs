namespace KuyumcuWebApi.dto;

public class UpdateUserRequestDto
{
    public int userId  { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public string Email { get; set; }
    public string Phone { get; set; }


}