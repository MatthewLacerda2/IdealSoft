using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BackEnd.Models;

namespace BackEnd.Data;

public class BackendDbContext : IdentityDbContext {

    public BackendDbContext(DbContextOptions<BackendDbContext> options) : base(options) {

    }

    protected override void OnModelCreating(ModelBuilder builder) {
        base.OnModelCreating(builder);

        builder.Entity<User>(entity => { entity.ToTable("Users"); });

    }

    public new DbSet<User> Users { get; set; }

}