using TopLearn.Core.ViewModels.AdminPanel;

namespace TopLearn.Core.Interfaces;

public interface IAdminPanelService
{
    UsersListViewModel GetUsers(int pageId = 1, string emailFilter = "", string userNameFilter = "");
    int AddUser(CreateUserViewModel user);
}