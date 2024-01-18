using Microsoft.AspNetCore.Identity;

namespace Chat.Identity.Models;

public class User: IdentityUser
{
    public string Name { get; set; }
}