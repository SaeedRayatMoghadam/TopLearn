namespace TopLearn.Core.DTOs.AdminPanel;

public class UsersDto
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public DateTime RegisterDate { get; set; }
    public bool IsActive { get; set; }
}