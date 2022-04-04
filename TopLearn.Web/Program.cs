using Microsoft.EntityFrameworkCore;
using TopLearn.Data.Context;

IConfiguration configuration = new ConfigurationBuilder()

  .AddJsonFile("appsettings.json")

  .Build();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();

//DataBase Setting
builder.Services.AddDbContext<TopLearnDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("TopLearnDBConnection"));
});

var app = builder.Build();

app.UseMvcWithDefaultRoute();
app.UseStaticFiles();

//app.MapGet("/", () => "Hello World!");

app.Run();
