using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Interfaces;
using TopLearn.Core.ViewModels.AdminPanel.Roles;

namespace TopLearn.Web.Pages.Admin.Roles
{
    public class CreateModel : PageModel
    {

        private readonly IAdminPanelService _adminPanelService;

        public CreateModel(IAdminPanelService adminPanelService)
        {
            _adminPanelService = adminPanelService;
        }

        [BindProperty]
        public RoleViewModel Role { get; set; }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            _adminPanelService.AddRole(Role);
            return Redirect("/Admin/Roles");
        }

    }
}
