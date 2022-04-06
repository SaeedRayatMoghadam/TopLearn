using TopLearn.Core.ViewModels.Account;
using TopLearn.Data.Models.Users;

namespace TopLearn.Core.Interfaces;

public interface IUserService
{
    bool IsUserNameExist(string userName);
    bool IsEmailExist(string email);
    int Register(User user);
    User Login(LoginViewModel model);

    bool ActivateAccount(string code);
}