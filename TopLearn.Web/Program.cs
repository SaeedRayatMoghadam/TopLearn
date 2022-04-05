using Microsoft.EntityFrameworkCore;
using TopLearn.Data.Context;

IConfiguration configuration = new ConfigurationBuilder()

  .AddJsonFile("appsettings.json")

  .Build();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

//DataBase Setting
builder.Services.AddDbContext<TopLearnDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("TopLearnDBConnection"));
});

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
