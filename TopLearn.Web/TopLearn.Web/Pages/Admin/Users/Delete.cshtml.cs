using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.DTOs.UserPanel;
using TopLearn.Core.Interfaces;

namespace TopLearn.Web.Pages.Admin.Users
{
    public class DeleteModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IAdminService _adminService;


        public DeleteModel(IUserService userService, IAdminService adminService)
        {
            _userService = userService;
            _adminService = adminService;
        }

        public UserInfoDto UserInfo{ get; set; }
        public void OnGet(int id)
        {
            ViewData["UserId"] = id;
            UserInfo = _userService.GetUserInfo(id);
        }

        public IActionResult OnPost(int UserId)
        {
            _adminService.DeleteUser(UserId);
            return RedirectToPage("Index");
        }
    }
}
