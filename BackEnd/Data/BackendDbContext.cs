using Microsoft.AspNetCore.Identity;
using System;


namespace BackEnd.Data;
public class BackendDbContext : IdentityDbContext {
    public BackendDbContext(DbContextOptions<BackendDbContext> options) : base(options) {
    }

    public DbSet<User> Users { get; set; }
}