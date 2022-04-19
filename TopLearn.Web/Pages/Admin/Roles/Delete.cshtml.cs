using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Interfaces;
using TopLearn.Core.ViewModels.AdminPanel.Roles;

namespace TopLearn.Web.Pages.Admin.Roles
{
    public class DeleteModel : PageModel
    {
        private readonly IAdminPanelService _adminPanelService;

        public DeleteModel(IAdminPanelService adminPanelService)
        {
            _adminPanelService = adminPanelService;
        }

        [BindProperty]
        public RoleViewModel Role { get; set; }
        
        public void OnGet(int id)
        {
            Role = _adminPanelService.GetRole(id);
        }

        public IActionResult OnPost()
        {
            _adminPanelService.DeleteRole(Role.Id);

            return Redirect("/Admin/Roles");
        }
    }
}
