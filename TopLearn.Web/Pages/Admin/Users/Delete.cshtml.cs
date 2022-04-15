using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Interfaces;
using TopLearn.Core.ViewModels.UserPanel;

namespace TopLearn.Web.Pages.Admin.Users
{
    public class DeleteModel : PageModel
    {
        private readonly IAdminPanelService _adminPanelService;
        private readonly IUserService _userService;

        public DeleteModel(IAdminPanelService adminPanelService, IUserService userService)
        {
            _adminPanelService = adminPanelService;
            _userService = userService;
        }

        public UserInfoViewModel UserInfoViewModel { get; set; }

        public void OnGet(int id)
        {
            ViewData["UserId"] = id;
            UserInfoViewModel = _userService.GetUserInfo(id);
        }

        public IActionResult OnPost(int UserId)
        {
            _adminPanelService.DeleteUser(UserId);
            return RedirectToPage("Index");
        }
    }
}
