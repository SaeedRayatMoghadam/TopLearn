using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Interfaces;
using TopLearn.Core.ViewModels.AdminPanel;

namespace TopLearn.Web.Pages.Admin.Users
{
    public class EditModel : PageModel
    {
        private readonly IAdminPanelService _adminPanelService;
        private readonly IPermissionService _permissionService;

        public EditModel(IAdminPanelService adminPanelService, IPermissionService permissionService)
        {
            _adminPanelService = adminPanelService;
            _permissionService = permissionService;
        }

        [BindProperty]
        [AllowNull]
        public EditUserViewModel EditUserViewModel { get; set; }


        public void OnGet(int id)
        {
            EditUserViewModel = _adminPanelService.GetUserForEdit(id);
            ViewData["Roles"] = _permissionService.GetRoles();
        }


        public IActionResult OnPost(List<int> SelectedRoles)
        {
            //Edit User
            _adminPanelService.EditUser(EditUserViewModel);

            //Edit User Roles
            _permissionService.EditUserRoles(EditUserViewModel.Id, SelectedRoles);

            return RedirectToPage("Index");
        }
    }
}
