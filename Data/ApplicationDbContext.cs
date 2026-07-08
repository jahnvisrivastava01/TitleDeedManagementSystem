using TitleDeedManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace TitleDeedManagementSystem.Data
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Role> Roles { get; set; }

    public DbSet<UserRole> UserRoles { get; set; }

    public DbSet<Branch> Branches { get; set; }

    public DbSet<Designation> Designations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      
      modelBuilder.Entity<UserRole>()
          .HasOne(ur => ur.User)
          .WithMany(u => u.UserRoles)
          .HasForeignKey(ur => ur.UserId);

     
      modelBuilder.Entity<UserRole>()
          .HasOne(ur => ur.Role)
          .WithMany(r => r.UserRoles)
          .HasForeignKey(ur => ur.RoleId);

      
      modelBuilder.Entity<User>()
          .HasOne(u => u.Designation)
          .WithMany(d => d.Users)
          .HasForeignKey(u => u.DesignationId);

     
      modelBuilder.Entity<User>()
          .HasOne(u => u.Branch)
          .WithMany(b => b.Users)
          .HasForeignKey(u => u.BranchId);

      modelBuilder.Entity<Role>().HasData(
        new Role { RoleId = 1, RoleName = "Maker" },
        new Role { RoleId = 2, RoleName = "Checker" },
        new Role { RoleId = 3, RoleName = "Branch Admin" });

      modelBuilder.Entity<Designation>().HasData(
        new Designation { DesignationId = 1, DesignationName = "Assistant Manager" },
        new Designation { DesignationId = 2, DesignationName = "Deputy Manager" },
        new Designation { DesignationId = 3, DesignationName = "Chief Manager" },
        new Designation { DesignationId = 4, DesignationName = "AGM" });

      modelBuilder.Entity<Branch>().HasData(
        new Branch { BranchId = 1, BranchName = "Head Office" },
        new Branch { BranchId = 2, BranchName = "Pune Branch" },
        new Branch { BranchId = 3, BranchName = "Nashik Branch" });
    



  }
  }
}
