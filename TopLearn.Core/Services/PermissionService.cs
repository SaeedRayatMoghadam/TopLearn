using TopLearn.Core.Interfaces;
using TopLearn.Data.Context;
using TopLearn.Data.Models.Users;

namespace TopLearn.Core.Services;

public class PermissionService : IPermissionService
{
    private readonly TopLearnDbContext _context;

    public PermissionService(TopLearnDbContext context)
    {
        _context = context;
    }

    public List<Role> GetRoles()
    {
        return _context.Roles.ToList();
    }

    public void AddRolesToUser(List<int> rolesId, int userId)
    {
        foreach (var roleID in rolesId)
        {
            _context.UserRoles.Add(new UserRole()
            {
                RoleId = roleID,
                UserId = userId
            });
            _context.SaveChanges();
        }
    }
}