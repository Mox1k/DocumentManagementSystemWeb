using DocumentManagementSystem.Data.Entites;
using Microsoft.EntityFrameworkCore;

namespace DocumentManagementSystem.Data.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();  
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string adminRoleName = "admin";
            string userRoleName = "user";
            
            Guid adminRoleId = Guid.NewGuid();
            Guid userRoleId = Guid.NewGuid();

            Role adminRole = new Role { Id = adminRoleId, Name = adminRoleName };
            Role userRole = new Role { Id = userRoleId, Name = userRoleName };

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            base.OnModelCreating(modelBuilder);
        }
    }
}

