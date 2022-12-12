using TopLearn.Core.DTOs.UserPanel;
using TopLearn.Core.Interfaces;
using TopLearn.Core.Security;
using TopLearn.Core.Utilities;
using TopLearn.Data.Context;
using TopLearn.Data.Entities.User;

namespace TopLearn.Core.Services;

public class UserService : IUserService
{
    private readonly TopLearnContext _context;
    private readonly IWalletService _walletService;


    public UserService(TopLearnContext context, IWalletService walletService)
    {
        _context = context;
        _walletService = walletService;
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
            Wallet = _walletService.GetUserWalletBalance(username)
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

    public void ChangePassword(string username, string password)
    {
        var user = GetUser(username);
        user.Password = PasswordHelper.EncodeToMd5(password);

        UpdateUser(user);
    }

    public bool ComparePassword(string username, string password)
    {
        return _context.Users.Any(u => u.UserName == username &&
                                       u.Password == PasswordHelper.EncodeToMd5(password));
    }

    public int GetUserId(string username)
    {
        return _context.Users.Single(u => u.UserName == username).Id;
    }
}