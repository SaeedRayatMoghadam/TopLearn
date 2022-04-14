using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Interfaces;
using TopLearn.Core.ViewModels.AdminPanel;

namespace TopLearn.Web.Pages.Admin.Users
{
    [BindProperties]
    public class CreateUserModel : PageModel
    {
        private readonly IPermissionService _permissionService;
        private readonly IAdminPanelService _adminPanelService;

        public CreateUserModel(IPermissionService permissionService, IAdminPanelService adminPanelService)
        {
            _permissionService = permissionService;
            _adminPanelService = adminPanelService;
        }

        public CreateUserViewModel CreateUserViewModel { get; set; }


        public void OnGet()
        {
            ViewData["Roles"] = _permissionService.GetRoles();
        }

        public IActionResult OnPost(List<int> SelectedRoles)
        {
            if (!ModelState.IsValid)
                return Page();

            int userId = _adminPanelService.AddUser(CreateUserViewModel);

            _permissionService.AddRolesToUser(SelectedRoles, userId);

            return Redirect("/Admin/Users");
        }
    }
}
