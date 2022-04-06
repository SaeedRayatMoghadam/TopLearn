using Microsoft.EntityFrameworkCore;
using TopLearn.Core.Interfaces;
using TopLearn.Core.Services;
using TopLearn.Data.Context;

IConfiguration configuration = new ConfigurationBuilder()

  .AddJsonFile("appsettings.json")

  .Build();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();


#region IOC

builder.Services.AddTransient<IUserService, UserService>();

#endregion


#region DataBase

builder.Services.AddDbContext<TopLearnDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("TopLearnDBConnection"));
});

#endregion

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
