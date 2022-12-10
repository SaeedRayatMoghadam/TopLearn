using TopLearn.Core.DTOs.UserPanel;
using TopLearn.Core.Interfaces;
using TopLearn.Core.Utilities;
using TopLearn.Data.Context;
using TopLearn.Data.Entities.User;

namespace TopLearn.Core.Services;

public class UserService : IUserService
{
    private readonly TopLearnContext _context;

    public UserService(TopLearnContext context)
    {
        _context = context;
    }

    

    public void EditProfile(string username, EditProfileViewModel model)
    {
        if (model.Image != null)
        {
            string imagePath = "";
            if (model.CurrentImage != "Default.jpg")
            {
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/UserAvatars",
                    model.CurrentImage);
                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }
            }

            model.CurrentImage = CodeGenerator.Generate() + Path.GetExtension(model.Image.FileName);
            imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/useravatars", model.CurrentImage);
            
            using var stream = new FileStream(imagePath, FileMode.Create);
            model.Image.CopyTo(stream);
        }

        var user = GetUser(username);

        user.UserName = model.UserName;
        user.Avatar = model.CurrentImage;
        user.Email = model.Email;

        UpdateUser(user);
    }

    public void UpdateUser(User user)
    {
        _context.Users.Update(user);
        _context.SaveChanges();
    }

    public User GetUser(string username)
    {
        return _context.Users.SingleOrDefault(u => u.UserName == username);
    }

    public EditProfileViewModel GetUserForEditProfile(string username)
    {
        return _context.Users.Where(u => u.UserName == username).Select(u => new EditProfileViewModel()
        {
            UserName = u.UserName,
            Email = u.Email,
            CurrentImage = u.Avatar
        }).Single();
    }

    public UserInfoDto GetUserInfo(string username)
    {
        var user = GetUser(username);

        var userInfo = new UserInfoDto()
        {
            UserName = user.UserName,
            Email = user.Email,
            RegisterDate = user.RegisterDate,
            Wallet = 0
        };

        return userInfo;
    }

    public UserPanelSideBarDto GetUserPanelSideBarInfo(string username)
    {
        return _context.Users.Where(u => u.UserName == username).Select(u => new UserPanelSideBarDto()
        {
            UserName = u.UserName,
            RegisterDate = u.RegisterDate,
            Image = u.Avatar
        }).Single();
    }

    public bool IsUserNameExist(string username)
    {
        return _context.Users.Any(u => u.UserName == username);
    }

    public bool IsEmailExist(string email)
    {
        return _context.Users.Any(u => u.Email == email);
    }
}