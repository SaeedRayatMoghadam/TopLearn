using TopLearn.Core.ViewModels.AdminPanel;
using TopLearn.Core.ViewModels.AdminPanel.Roles;
using TopLearn.Data.Models.Users;

namespace TopLearn.Core.Interfaces;

public interface IAdminPanelService
{
    //USERS
    UsersListViewModel GetUsers(int pageId = 1, string emailFilter = "", string userNameFilter = "");
    UsersListViewModel GetDeletedUsers(int pageId = 1, string emailFilter = "", string userNameFilter = "");
    int AddUser(CreateUserViewModel user);
    EditUserViewModel GetUserForEdit(int userId);
    void EditUser(EditUserViewModel editedUser);
    void DeleteUser(int userId);

    //ROLES
    RoleViewModel GetRole(int id);
    List<RoleViewModel> GetRoles();
    void AddRole(RoleViewModel role);
    void UpdateRole(RoleViewModel role);
    void DeleteRole(int id);
}