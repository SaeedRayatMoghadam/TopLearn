using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using TopLearn.Core.Interfaces;
using TopLearn.Core.Services;
using TopLearn.Core.Utils;
using TopLearn.Data.Context;

IConfiguration configuration = new ConfigurationBuilder()

  .AddJsonFile("appsettings.json")

  .Build();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();


#region Authentication

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.LoginPath = "/Login";
    options.LogoutPath = "/Logout";
    options.ExpireTimeSpan = TimeSpan.FromMilliseconds(43200);
});

#endregion

#region DataBase

builder.Services.AddDbContext<TopLearnDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("TopLearnDBConnection"));
});

#endregion

#region IOC

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IWalletService, WalletService>();
builder.Services.AddTransient<IAdminPanelService, AdminPanelService>();
builder.Services.AddTransient<IPermissionService, PermissionService>();
builder.Services.AddTransient<IViewRenderService, RenderViewToString>();


builder.Services.AddTransient<ICategoryService, CategoryService>();

#endregion

var app = builder.Build();

app.UseStaticFiles();

app.UseAuthentication();

app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{Id?}");
app.MapRazorPages();

app.Run();
