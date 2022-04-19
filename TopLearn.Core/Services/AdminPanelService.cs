using Microsoft.EntityFrameworkCore;
using TopLearn.Core.DTOs.AdminPanel;
using TopLearn.Core.Interfaces;
using TopLearn.Core.Utils;
using TopLearn.Core.ViewModels.AdminPanel;
using TopLearn.Core.ViewModels.AdminPanel.Roles;
using TopLearn.Data.Context;
using TopLearn.Data.Models.Users;

namespace TopLearn.Core.Services;

public class AdminPanelService : IAdminPanelService
{
    private readonly TopLearnDbContext _context;
    private readonly IUserService _userService;

    public AdminPanelService(TopLearnDbContext context, IUserService userService)
    {
        _context = context;
        _userService = userService;
    }

    public UsersListViewModel GetUsers(int pageId = 1, string emailFilter = "", string userNameFilter = "")
    {
        IQueryable<User> result = _context.Users;

        if (!string.IsNullOrEmpty(emailFilter))
            result = result.Where(u => u.Email.Contains(emailFilter));

        if (!string.IsNullOrEmpty(userNameFilter))
            result = result.Where(u => u.UserName.Contains(userNameFilter));

        int take = 1;
        int skip = (pageId - 1) * take;

        return new UsersListViewModel()
        {
            Users = result.OrderBy(u => u.RegisterDate).Skip(skip).Take(take)
                .Select(u => new UsersDto()
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    RegisterDate = u.RegisterDate,
                    IsActive = u.IsActive
                })
                .ToList(),
            CurrentPage = pageId,
            PageCount = result.Count() / take
        };
    }

    public UsersListViewModel GetDeletedUsers(int pageId = 1, string emailFilter = "", string userNameFilter = "")
    {
        IQueryable<User> result = _context.Users.IgnoreQueryFilters().Where(u => u.IsDeleted);

        if (!string.IsNullOrEmpty(emailFilter))
            result = result.Where(u => u.Email.Contains(emailFilter));

        if (!string.IsNullOrEmpty(userNameFilter))
            result = result.Where(u => u.UserName.Contains(userNameFilter));

        int take = 1;
        int skip = (pageId - 1) * take;

        return new UsersListViewModel()
        {
            Users = result.OrderBy(u => u.RegisterDate).Skip(skip).Take(take)
                .Select(u => new UsersDto()
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    RegisterDate = u.RegisterDate,
                    IsActive = u.IsActive
                })
                .ToList(),
            CurrentPage = pageId,
            PageCount = result.Count() / take
        };
    }

    public int AddUser(CreateUserViewModel user)
    {
        var newUser = new User()
        {
            UserName = user.UserName,
            Email = user.Email,
            Password = user.Password.EncodeToMd5(),
            RegisterDate = DateTime.Now,
            ActiveCode = Generator.GenerateGuid(),
            IsActive = true,
            Avatar = "Default.jpg",
        };
        
        _context.Users.Add(newUser);
        _context.SaveChanges();

        return newUser.Id;
    }

    public EditUserViewModel GetUserForEdit(int userId)
    {
        return _context.Users
            .Include(u => u.UserRoles)
            .Where(u => u.Id == userId)
            .Select(u => new EditUserViewModel()
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                Roles = u.UserRoles.Select(r => r.RoleId).ToList()
            }).Single();
    }

    public void EditUser(EditUserViewModel editedUser)
    {
        var user = _userService.GetByUserName(editedUser.UserName);

        if (!string.IsNullOrEmpty(editedUser.Email))
            user.Email = editedUser.Email;

        if (!string.IsNullOrEmpty(editedUser.Password))
            user.Password = editedUser.Password.EncodeToMd5();  

        _context.Users.Update(user);
        _context.SaveChanges();
    }

    public void DeleteUser(int userId)
    {
        var user = _context.Users.Single(u => u.Id == userId);

        user.IsDeleted = true;

        _context.Users.Update(user);
        _context.SaveChanges();
    }

    public List<RoleViewModel> GetRoles()
    {
        return _context.Roles.Select(r => new RoleViewModel()
        {
            Id = r.Id,
            Title = r.Title
        }).ToList();
    }

    public void AddRole(RoleViewModel role)
    {
        _context.Roles.Add(new Role()
        {
            Title = role.Title
        });

        _context.SaveChanges();
    }

    public void UpdateRole(RoleViewModel role)
    {
        _context.Roles.Update(new Role()
        {
            Id = role.Id,
            Title = role.Title
        });
        _context.SaveChanges(); 
    }

    public void DeleteRole(int id)
    {
        var role = _context.Roles.Single(r => r.Id == id);

        _context.Roles.Remove(role);
        _context.SaveChanges();
    }

    public RoleViewModel GetRole(int id)
    {
        return _context.Roles.Where(r => r.Id == id).Select(r => new RoleViewModel()
        {
            Id = r.Id,
            Title = r.Title
        }).Single();
    }
}