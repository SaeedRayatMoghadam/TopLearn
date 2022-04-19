using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Interfaces;
using TopLearn.Core.ViewModels.AdminPanel.Roles;

namespace TopLearn.Web.Pages.Admin.Roles
{
    public class EditModel : PageModel
    {
        private readonly IAdminPanelService _adminPanelService;

        public EditModel(IAdminPanelService adminPanelService)
        {
            _adminPanelService = adminPanelService;
        }

        [BindProperty] public RoleViewModel Role { get; set; }

        public void OnGet(int id)
        {
            Role = _adminPanelService.GetRole(id);
        }

        public IActionResult OnPost()
        {
            _adminPanelService.UpdateRole(Role);
            return Redirect("/Admin/Roles");
        }

    }
}
