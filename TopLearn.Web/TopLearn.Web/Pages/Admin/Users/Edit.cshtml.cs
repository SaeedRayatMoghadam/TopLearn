using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.DTOs.AdminPanel;
using TopLearn.Core.Interfaces;

namespace TopLearn.Web.Pages.Admin.Users
{
    public class EditModel : PageModel
    {
        private readonly IAdminService _adminService;

        public EditModel(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [BindProperty]
        public EditUserViewModel EditUserViewModel { get; set; }

        public void OnGet(int id)
        {
            ViewData["Roles"] = _adminService.GetRoles();
            EditUserViewModel = _adminService.GetUserForAdminEdit(id);
        }

        public IActionResult OnPost(List<int> SelectedRoles)
        {
            if (!ModelState.IsValid)
            {
                EditUserViewModel = _adminService.GetUserForAdminEdit(EditUserViewModel.UserId);
                return RedirectToPage("Edit", new { id = EditUserViewModel.UserId });
            }

            _adminService.EditUser(EditUserViewModel);
            _adminService.EditUserRoles(SelectedRoles, EditUserViewModel.UserId);

            return Redirect("Index");
        }
    }
}
