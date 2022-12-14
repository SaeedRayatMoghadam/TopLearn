using TopLearn.Core.DTOs.AdminPanel;
using TopLearn.Core.Interfaces;
using TopLearn.Core.Security;
using TopLearn.Core.Utilities;
using TopLearn.Data.Context;
using TopLearn.Data.Entities.User;

namespace TopLearn.Core.Services;

public class AdminService : IAdminService
{
    private readonly TopLearnContext _context;
    private readonly IAccountService _accountService;

    public AdminService(TopLearnContext context, IAccountService accountService)
    {
        _context = context;
        _accountService = accountService;
    }

    public int AddUser(CreateUserViewModel user)
    {
        var newUser = new User();

        newUser.UserName = user.UserName;
        newUser.Email = user.Email;
        newUser.Password = PasswordHelper.EncodeToMd5(user.Password);
        newUser.IsActive = true;
        newUser.RegisterDate = DateTime.Now;
        newUser.ActiveCode = CodeGenerator.Generate();


        if (user.Image != null)
        {
            
        newUser.Avatar = CodeGenerator.Generate() + Path.GetExtension(user.Image.FileName);
        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/useravatars", newUser.Avatar);

        using var stream = new FileStream(imagePath, FileMode.Create);
        user.Image.CopyTo(stream);
        }

        return _accountService.Register(newUser);
    }

    public void AddUserRoles(List<int> roleIds, int userId)
    {
        foreach (var id in roleIds)
        {
            _context.UserRoles.Add(new UserRole()
            {
                RoleId = id,
                UserId = userId
            });
            _context.SaveChanges();
        }
    }

    public List<Role> GetRoles()
    {
        return _context.Roles.ToList();
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