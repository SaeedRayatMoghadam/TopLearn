using TopLearn.Data.Entities.User;

namespace TopLearn.Core.Interfaces;

public interface IAccountService
{
    bool IsUserNameExist(string userName);
    bool IsEmailExist(string email);

    int Register(User user);
}