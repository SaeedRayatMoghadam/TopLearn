using TopLearn.Data.Entities.User;

namespace TopLearn.Core.DTOs.AdminPanel;

public class UserManagementViewModel
{
    public List<User> Users { get; set; }
    public int CurrentPage { get; set; }
    public int PageCount { get; set; }
}