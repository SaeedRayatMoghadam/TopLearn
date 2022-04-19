using TopLearn.Core.ViewModels.AdminPanel.Roles;
using TopLearn.Data.Models.Users;

namespace TopLearn.Core.Interfaces;

public interface IPermissionService
{
    List<Role> GetRoles();
    List<int> GetUserRoles(int userId);
    void AddRolesToUser(List<int> rolesId, int userId);
    void EditUserRoles(int userId, List<int> rolesId);
}