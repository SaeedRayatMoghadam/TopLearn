using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Interfaces;
using TopLearn.Core.ViewModels.AdminPanel.Roles;

namespace TopLearn.Web.Pages.Admin.Roles
{
    public class IndexModel : PageModel
    {
        private readonly IAdminPanelService _adminPanelService;

        public IndexModel(IAdminPanelService adminPanelService)
        {
            _adminPanelService = adminPanelService;
        }

        public List<RoleViewModel> Roles { get; set; }

        public void OnGet()
        {
            Roles = _adminPanelService.GetRoles();
        }
    }
}
