using TopLearn.Core.DTOs.AdminPanel;
using TopLearn.Data.Entities.User;

namespace TopLearn.Core.Interfaces;

public interface IAdminService
{
    UserManagementViewModel GetUsers(int pageId=1, string emailFilter="",string usernameFilter="");
    int AddUser(CreateUserViewModel user);
    List<Role> GetRoles();
    void AddUserRoles(List<int> roleIds, int userId);
} 