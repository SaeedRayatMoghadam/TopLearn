using TopLearn.Core.DTOs.AdminPanel;
using TopLearn.Core.Interfaces;
using TopLearn.Core.Utils;
using TopLearn.Core.ViewModels.AdminPanel;
using TopLearn.Data.Context;
using TopLearn.Data.Models.Users;

namespace TopLearn.Core.Services;

public class AdminPanelService : IAdminPanelService
{
    private readonly TopLearnDbContext _context;

    public AdminPanelService(TopLearnDbContext context)
    {
        _context = context;
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
}