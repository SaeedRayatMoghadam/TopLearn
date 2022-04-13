using TopLearn.Core.DTOs.AdminPanel;

namespace TopLearn.Core.ViewModels.AdminPanel;

public class UsersListViewModel
{
    public List<UsersDto> Users { get; set; }
    public int CurrentPage { get; set; }
    public int PageCount { get; set; }
}