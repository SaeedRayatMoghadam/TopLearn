using Microsoft.AspNetCore.Mvc.ActionConstraints;
using TopLearn.Core.Interfaces;
using TopLearn.Core.Utils;
using TopLearn.Core.ViewModels.Account;
using TopLearn.Data.Context;
using TopLearn.Data.Models.Users;

namespace TopLearn.Core.Services;

public class UserService : IUserService
{
    private readonly TopLearnDbContext _context;

    public UserService(TopLearnDbContext context)
    {
        _context = context;
    }

    public User Get(string email)
    {
        return _context.Users.SingleOrDefault(x => x.Email == email);
    }

    public User GetByActiveCode(string activeCode)
    {
        return _context.Users.SingleOrDefault(x => x.ActiveCode == activeCode);
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
}