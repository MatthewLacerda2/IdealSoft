using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BackEnd.Models;

namespace BackEnd.Data;

public class BackendDbContext : IdentityDbContext {

    public BackendDbContext(DbContextOptions<BackendDbContext> options) : base(options) {

    }

    public DbSet<User> Users { get; set; }

}