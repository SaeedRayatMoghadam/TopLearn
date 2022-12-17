using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.DTOs.AdminPanel;
using TopLearn.Core.Interfaces;

namespace TopLearn.Web.Pages.Admin.Users
{
    public class DeletedUsersModel : PageModel
    {
        private readonly IAdminService _adminService;

        public DeletedUsersModel(IAdminService adminService)
        {
            _adminService = adminService;
        }


        public UserManagementViewModel Users { get; set; }
        public void OnGet(int pageId = 1, string emailFilter = "", string usernameFilter = "")
        {
            Users = _adminService.GetDeletedUsers(pageId, emailFilter, usernameFilter);
        }
    }
}
