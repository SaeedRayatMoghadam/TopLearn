using TopLearn.Data.Models.Users;

namespace TopLearn.Core.Interfaces;

public interface IPermissionService
{
    List<Role> GetRoles();
    void AddRolesToUser(List<int> rolesId, int userId);
}