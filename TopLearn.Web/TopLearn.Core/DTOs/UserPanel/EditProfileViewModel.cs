using Microsoft.AspNetCore.Http;

namespace TopLearn.Core.DTOs.UserPanel;

public class EditProfileViewModel
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public IFormFile Image { get; set; }
    public string CurrentImage { get; set; }
}