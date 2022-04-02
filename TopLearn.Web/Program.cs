var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Services.AddMvc();


app.UseMvcWithDefaultRoute();
app.UseStaticFiles();

//app.MapGet("/", () => "Hello World!");

app.Run();
