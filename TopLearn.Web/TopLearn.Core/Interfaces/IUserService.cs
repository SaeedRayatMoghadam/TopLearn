using TopLearn.Core.DTOs.UserPanel;
using TopLearn.Data.Entities.User;

namespace TopLearn.Core.Interfaces;

public interface IUserService
{
    User GetUser(string username);
    EditProfileViewModel GetUserForEditProfile(string username);
    UserInfoDto GetUserInfo(string username);
    UserPanelSideBarDto GetUserPanelSideBarInfo(string username);

    bool IsUserNameExist(string username);
    bool IsEmailExist(string email);

    void EditProfile(string username, EditProfileViewModel model);
    void UpdateUser(User user);
}