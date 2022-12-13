using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.DTOs.AdminPanel;
using TopLearn.Core.Interfaces;

namespace TopLearn.Web.Pages.Admin.Users
{
    public class IndexModel : PageModel
    {
        private readonly IAdminService _adminService;

        public IndexModel(IAdminService adminService)
        {
            _adminService = adminService;
        }


        public UserManagementViewModel Users { get; set; }
        public void OnGet(int pageId=1, string emailFilter="",string usernameFilter="")
        {
            Users = _adminService.GetUsers(pageId, emailFilter, usernameFilter);
        }
    }
}
