using TopLearn.Core.DTOs.Account;
using TopLearn.Core.Interfaces;
using TopLearn.Core.Security;
using TopLearn.Core.Utilities;
using TopLearn.Data.Context;
using TopLearn.Data.Entities.User;

namespace TopLearn.Core.Services;

public class AccountService : IAccountService
{
    private TopLearnContext _context;

    public AccountService(TopLearnContext context)
    {
        _context = context;
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

    public User Login(LoginDto user)
    {
        var pass = PasswordHelper.EncodeToMd5(user.Password);
        var email = EmailFixer.Fix(user.Email);
        
        return _context.Users.SingleOrDefault(u => u.Email == email && u.Password == pass);
    }

    public bool ActiveAccount(string activeCode)
    {
        var user = _context.Users.SingleOrDefault(u => u.ActiveCode == activeCode);
        if (user == null || user.IsActive)
            return false;

        user.IsActive = true;
        user.ActiveCode = CodeGenerator.Generate();
        _context.SaveChanges();

        return true;
    }
}