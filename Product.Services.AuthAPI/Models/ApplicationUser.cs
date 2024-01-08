using Microsoft.AspNetCore.Identity;

namespace Product.Services.AuthAPI.Models;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; }
    
}