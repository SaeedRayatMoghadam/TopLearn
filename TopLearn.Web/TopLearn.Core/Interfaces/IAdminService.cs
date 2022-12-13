using TopLearn.Core.DTOs.AdminPanel;

namespace TopLearn.Core.Interfaces;

public interface IAdminService
{
    UserManagementViewModel GetUsers(int pageId=1, string emailFilter="",string usernameFilter="");
}