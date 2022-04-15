using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Interfaces;
using TopLearn.Core.ViewModels.AdminPanel;

namespace TopLearn.Web.Pages.Admin.Users
{
    public class DeletedUsersModel : PageModel
    {
        private readonly IAdminPanelService _adminPanelService;

        public DeletedUsersModel(IAdminPanelService adminPanelService)
        {
            _adminPanelService = adminPanelService;
        }

        public UsersListViewModel UsersListViewModel { get; set; }

        public void OnGet(int pageId = 1, string emailFilter = "", string userNameFilter = "")
        {
            UsersListViewModel = _adminPanelService.GetDeletedUsers(pageId, emailFilter, userNameFilter);
        }
    }
}
