using Chat.Identity;
using Chat.Identity.Data;
using Chat.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var dbconnstr = builder.Configuration.GetValue<string>("DbConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(dbconnstr);
});

builder.Services
    .AddIdentity<User, IdentityRole>(config =>
    {
        config.Password.RequiredLength = 1;
        config.Password.RequireDigit = false;
        config.Password.RequireLowercase = false;
        config.Password.RequireUppercase = false;
        config.Password.RequireNonAlphanumeric = false;
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();
    
builder.Services.AddIdentityServer()
    .AddAspNetIdentity<User>()
    .AddInMemoryApiResources(Config.ApiResources)
    .AddInMemoryIdentityResources(Config.IdentityResources)
    .AddInMemoryApiScopes(Config.ApiScopes)
    .AddInMemoryClients(Config.Clients)
    .AddDeveloperSigningCredential();

builder.Services.ConfigureApplicationCookie(config =>
{
    config.Cookie.Name = "Char.Identity.Cookie";
    config.LoginPath = "/Auth/Login";
    config.LogoutPath = "/Auth/Logout";
});

var app = builder.Build();

await using (app.Services.CreateAsyncScope())
{
    try
    {
        var context = app.Services.GetRequiredService<AppDbContext>();
        await DbInitializer.InitializeAsync(context);
    }
    catch (Exception e)
    {
        var logger = app.Services.GetRequiredService<ILogger<Program>>();
        logger.LogError(e, "An error occurred while app initialization");
    }
}

app.UseIdentityServer();

app.MapPost("/Login", async (HttpContext context, LoginViewModel login) =>
{
    var userManager = context.RequestServices.GetRequiredService<UserManager<User>>();
    var user = await userManager.FindByNameAsync(login.Name);
    if (user is null)
    {
        context.Response.StatusCode = StatusCodes.Status404NotFound;
        return;
    }
    var signinManager = context.RequestServices.GetRequiredService<SignInManager<User>>();
    var result = signinManager.PasswordSignInAsync(
        login.Name, 
        login.Password,
        false,
        false).Result;
    if (result.Succeeded)
    {
        context.Response.StatusCode = StatusCodes.Status200OK;
        return;
    }
    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
});

app.Run();