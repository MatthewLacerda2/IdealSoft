using Microsoft.AspNetCore.Identity;

public class User : IdentityUser {
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
}