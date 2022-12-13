using TopLearn.Core.DTOs.AdminPanel;
using TopLearn.Core.Interfaces;
using TopLearn.Data.Context;
using TopLearn.Data.Entities.User;

namespace TopLearn.Core.Services;

public class AdminService : IAdminService
{
    private readonly TopLearnContext _context;

    public AdminService(TopLearnContext context)
    {
        _context = context;
    }

    public UserManagementViewModel GetUsers(int pageId = 1, string emailFilter = "", string usernameFilter = "")
    {
        IQueryable<User> users = _context.Users;

        if (!string.IsNullOrEmpty(emailFilter))
            users = users.Where(u => u.Email.Contains(emailFilter));

        if (!string.IsNullOrEmpty(usernameFilter))
            users = users.Where(u => u.UserName.Contains(usernameFilter));

        int take = 1;
        int skip = (pageId - 1) * take;

        var result = new UserManagementViewModel();
        result.CurrentPage = pageId;
        result.PageCount = users.Count() / take;
        result.Users = users.OrderBy(u => u.RegisterDate).Skip(skip).Take(take).ToList();

        return result;
    }
}