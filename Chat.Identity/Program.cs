using Chat.Identity;
using Chat.Identity.Data;
using Chat.Identity.Filters;
using Chat.Identity.Models;
using Chat.Identity.Validators;
using FluentValidation;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IValidator<LoginViewModel>, LoginValidator>();
builder.Services.AddScoped<IValidator<RegisterViewModel>, RegisterValidator>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
    config.LoginPath = "/Login";
    config.LogoutPath = "/Logout";
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    try
    {
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        DbInitializer.Initialize(context);
    }
    catch (Exception e)
    {
        var logger = app.Services.GetRequiredService<ILogger<Program>>();
        logger.LogError(e, "An error occurred while app initialization");
    }
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseIdentityServer();

app.MapPost("/Login", async (HttpContext context, LoginViewModel login) =>
{
    var userManager = context.RequestServices.GetRequiredService<UserManager<User>>();
    var user = await userManager.FindByNameAsync(login.Name);
    if (user is null)
        return Results.NotFound("User not found");
    var signinManager = context.RequestServices.GetRequiredService<SignInManager<User>>();
    var result = signinManager.PasswordSignInAsync(
        login.Name, 
        login.Password,
        false,
        false).Result;
    if (result.Succeeded)
        return Results.Ok(result);

    return Results.BadRequest("Incorrect name or password");
})
    .AddEndpointFilter<LoginValidationFilter>();


app.MapPost("/Register", async (HttpContext context, RegisterViewModel register) =>
{
    var userManager = context.RequestServices.GetRequiredService<UserManager<User>>();
    var user = await userManager.FindByNameAsync(register.Name);
    if (user is not null)
        return Results.Conflict("User already exists");

    user = new()
    {
        UserName = register.Name,  // Обязательное поле
        Name = register.Name
    };

    var result = await userManager.CreateAsync(user, register.Password);
    if (result.Succeeded)
    {
        var signinManager = context.RequestServices.GetRequiredService<SignInManager<User>>();
        await signinManager.SignInAsync(user, false);
        return Results.Ok();
    }

    return Results.Problem("An error occurred");
})
    .AddEndpointFilter<RegisterValidationFilter>();

app.MapPost("/Logout", async (HttpContext context, string logoutId) =>
{
    var signinManager = context.RequestServices.GetRequiredService<SignInManager<User>>();
    await signinManager.SignOutAsync();

    var interactionService = context.RequestServices.GetRequiredService<IIdentityServerInteractionService>();
    var logoutRequest = await interactionService.GetLogoutContextAsync(logoutId);
    return Results.Redirect(logoutRequest.PostLogoutRedirectUri);
});

app.Run();
