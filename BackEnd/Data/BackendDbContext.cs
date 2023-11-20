using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Data;
public class BackendDbContext : IdentityDbContext {

    public BackendDbContext(DbContextOptions<BackendDbContext> options) : base(options) {
        
    }

    public DbSet<User> Users { get; set; }

}