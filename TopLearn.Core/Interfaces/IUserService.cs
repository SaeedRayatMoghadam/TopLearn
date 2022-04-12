using TopLearn.Core.ViewModels.Account;
using TopLearn.Core.ViewModels.UserPanel;
using TopLearn.Data.Models.Users;

namespace TopLearn.Core.Interfaces;

public interface IUserService
{
    User Get(string email);
    User GetByActiveCode(string activeCode);
    int GetUserId(string userName);
    
    //UserPanel Methods
    User GetByUserName(string userName);
    UserInfoViewModel GetUserInfo(string userName);
    UserPanelSideBarViewModel GetUserPanelSideBarInfo(string userName);
    EditUserProfileViewModel GetUserForEdit(string userName);
    void EditProfile(string userName, EditUserProfileViewModel profile);
    bool CheckPassword(string userName, string password);
    void ChangePassword(string userName, string password);

    void Update(User user);
    
    bool IsUserNameExist(string userName);
    bool IsEmailExist(string email);
    int Register(User user);
    User Login(LoginViewModel model);

    bool ActivateAccount(string code);
}