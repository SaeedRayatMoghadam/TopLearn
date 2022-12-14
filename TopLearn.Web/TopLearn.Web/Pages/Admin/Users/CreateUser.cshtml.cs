using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.DTOs.AdminPanel;
using TopLearn.Core.Interfaces;

namespace TopLearn.Web.Pages.Admin.Users
{
    public class CreateUserModel : PageModel
    {
        private readonly IAdminService _adminService;

        public CreateUserModel(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [BindProperty]
        public CreateUserViewModel CreateUserViewModel { get; set; }
        public void OnGet()
        {
            ViewData["Roles"] = _adminService.GetRoles();
        }

        public IActionResult OnPost(List<int> SelectedRoles)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Roles"] = _adminService.GetRoles();
                return Page();
            }

            var userId = _adminService.AddUser(CreateUserViewModel);
            _adminService.AddUserRoles(SelectedRoles,userId);

            return Redirect("/Admin/Users");
        }
    }
}
