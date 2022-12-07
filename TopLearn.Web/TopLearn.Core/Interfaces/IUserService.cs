using TopLearn.Core.DTOs.UserPanel;
using TopLearn.Data.Entities.User;

namespace TopLearn.Core.Interfaces;

public interface IUserService
{
    User GetUser(string username);
    UserInfoDto GetUserInfo(string username);
    UserPanelSideBarDto GetUserPanelSideBarInfo(string username);
}