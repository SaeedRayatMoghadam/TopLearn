using TopLearn.Core.DTOs.AdminPanel;
using TopLearn.Data.Entities.User;

namespace TopLearn.Core.Interfaces;

public interface IAdminService
{
    UserManagementViewModel GetUsers(int pageId=1, string emailFilter="",string usernameFilter="");
    EditUserViewModel GetUserForAdminEdit(int userId);

    int AddUser(CreateUserViewModel user);
    void EditUser(EditUserViewModel user);
    List<Role> GetRoles();
    void AddUserRoles(List<int> roleIds, int userId);
    void EditUserRoles(List<int> roleIds, int userId);
} 