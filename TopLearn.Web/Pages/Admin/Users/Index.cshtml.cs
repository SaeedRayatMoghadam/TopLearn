using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Interfaces;
using TopLearn.Core.ViewModels.AdminPanel;

namespace TopLearn.Web.Pages.Admin.Users
{
    public class IndexModel : PageModel
    {
        private readonly IAdminPanelService _adminPanelService;

        public IndexModel(IAdminPanelService adminPanelService)
        {
            _adminPanelService = adminPanelService;
        }

        public UsersListViewModel UsersListViewModel { get; set; }

        public void OnGet(int pageId = 1, string emailFilter = "", string userNameFilter = "")
        {
            UsersListViewModel = _adminPanelService.GetUsers(pageId,emailFilter,userNameFilter);
        }
    }
}
