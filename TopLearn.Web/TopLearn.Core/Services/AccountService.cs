using TopLearn.Core.Interfaces;
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
}