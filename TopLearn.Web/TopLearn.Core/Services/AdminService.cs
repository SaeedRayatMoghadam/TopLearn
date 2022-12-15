using Microsoft.EntityFrameworkCore;
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

    public void EditUserRoles(List<int> roleIds, int userId)
    {
        _context.UserRoles.Where(ur => ur.UserId == userId).ToList()
            .ForEach(ur => _context.UserRoles.RemoveRange(ur));

        AddUserRoles(roleIds, userId);
    }

    public void EditUser(EditUserViewModel user)
    {
        var editUser = _context.Users.Find(user.UserId);

        editUser.Email = user.Email;

        if (!string.IsNullOrEmpty(user.Password))
            editUser.Password = PasswordHelper.EncodeToMd5(user.Password);

        if (user.Image != null)
        {
            if (user.CurrentImage != "Default.jpg")
            {
                var currentImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/usersavatar",
                    user.CurrentImage);
                if (File.Exists(currentImagePath))
                    File.Delete(currentImagePath);
            }

            editUser.Avatar = CodeGenerator.Generate() + Path.GetExtension(user.Image.FileName);
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/usersavatar",
                editUser.Avatar);
            using var stream = new FileStream(imagePath, FileMode.Create);
            stream.CopyTo(stream);
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

    public EditUserViewModel GetUserForAdminEdit(int userId)
    {
        return _context.Users.Where(u => u.Id == userId)
            .Include(u => u.UserRoles)
            .Select(u => new EditUserViewModel()
            {
                UserId = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                CurrentImage = u.Avatar,
                Roles = u.UserRoles.Select(r => r.Id).ToList()
            }).Single();
    }
}