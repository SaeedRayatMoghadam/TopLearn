using TopLearn.Core.DTOs.UserPanel;
using TopLearn.Core.Interfaces;
using TopLearn.Data.Context;
using TopLearn.Data.Entities.User;

namespace TopLearn.Core.Security;

public class UserService : IUserService
{
    private readonly TopLearnContext _context;

    public UserService(TopLearnContext context)
    {
        _context = context;
    }

    public User GetUser(string username)
    {
        return _context.Users.SingleOrDefault(u => u.UserName == username);
    }

    public UserInfoDto GetUserInfo(string username)
    {
        var user = GetUser(username);

        var userInfo = new UserInfoDto()
        {
            UserName = user.UserName,
            Email = user.Email,
            RegisterDate = user.RegisterDate,
            Wallet = 0
        };

        return userInfo;
    }

    public UserPanelSideBarDto GetUserPanelSideBarInfo(string username)
    {
        return _context.Users.Where(u => u.UserName == username).Select(u => new UserPanelSideBarDto()
        {
            UserName = u.UserName,
            RegisterDate = u.RegisterDate,
            Image = u.Avatar
        }).Single();
    }
}