using Microsoft.AspNetCore.Mvc.ActionConstraints;
using TopLearn.Core.Interfaces;
using TopLearn.Core.Utils;
using TopLearn.Core.ViewModels.Account;
using TopLearn.Core.ViewModels.UserPanel;
using TopLearn.Data.Context;
using TopLearn.Data.Models.Users;

namespace TopLearn.Core.Services;

public class UserService : IUserService
{
    private readonly TopLearnDbContext _context;
    private readonly IWalletService _waleService;

    public UserService(TopLearnDbContext context, IWalletService waleService)
    {
        _context = context;
        _waleService = waleService;
    }

    public User Get(string email)
    {
        return _context.Users.SingleOrDefault(x => x.Email == email);
    }

    public User GetByActiveCode(string activeCode)
    {
        return _context.Users.SingleOrDefault(x => x.ActiveCode == activeCode);
    }

    public int GetUserId(string userName)
    {
        return _context.Users.Single(u => u.UserName == userName).Id;
    }

    public UserPanelSideBarViewModel GetUserPanelSideBarInfo(string userName)
    {
        return _context.Users.Where(u => u.UserName == userName).Select(u => new UserPanelSideBarViewModel()
        {
            UserName = u.UserName,
            RegisterDate = u.RegisterDate,
            Avatar = u.Avatar
        }).Single();
    }

    public EditUserProfileViewModel GetUserForEdit(string userName)
    {
        return _context.Users.Where(u => u.UserName == userName).Select(u => new EditUserProfileViewModel()
        {
            UserName = u.UserName,
            Email = u.Email
        }).Single();
    }

    public bool CheckPassword(string userName, string password)
    {
        var pass = password.EncodeToMd5();
        return _context.Users.Any(u => u.UserName == userName && u.Password == pass);
    }

    public void ChangePassword(string userName, string password)
    {
        var user = GetByUserName(userName);
        user.Password = password.EncodeToMd5();

        Update(user);
    }

    public void Update(User user)
    {
        _context.Users.Update(user);
        _context.SaveChanges();
    }

    public bool IsUserNameExist(string userName)
    {
        return _context.Users.Any(u => u.UserName == userName);
    }

    public bool IsEmailExist(string email)
    {
        return _context.Users.Any(u => u.Email == email);
    }

    public int Register(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();

        return user.Id;
    }

    public User Login(LoginViewModel model)
    {
        string pass = model.Password.EncodeToMd5();
        string email = model.Email.FixEmail();

        return _context.Users.SingleOrDefault(u => u.Email == email && u.Password == pass);
    }

    public bool ActivateAccount(string code)
    {
        var user = _context.Users.SingleOrDefault(u => u.ActiveCode == code);

        if (user == null || user.IsActive)
            return false;

        user.IsActive = true;
        user.ActiveCode = Generator.GenerateGuid();

        _context.SaveChanges();

        return true;
    }

    public User GetByUserName(string userName)
    {
        return _context.Users.SingleOrDefault(u => u.UserName == userName);
    }

    public UserInfoViewModel GetUserInfo(string userName)
    {
        var user = GetByUserName(userName);

        return new UserInfoViewModel()
        {
            UserName = user.UserName,
            Email = user.Email,
            RegisterDate = user.RegisterDate,
            Wallet = _waleService.GetWalletBalance(user.Id)
        };
    }

    public void EditProfile(string userName, EditUserProfileViewModel profile)
    {
        var user = GetByUserName(userName);

        user.UserName = profile.UserName;
        user.Email = profile.Email;

        Update(user);
    }
}