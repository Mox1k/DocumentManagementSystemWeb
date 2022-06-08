using DocumentManagementSystem.Data.Entites;
using Microsoft.EntityFrameworkCore;

namespace DocumentManagementSystem.Data.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<Client> Clients { get; set; } = null!;
        public DbSet<Document> Documents { get; set; } = null!;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();  
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string directorRoleName = "Директор";
            string managerRoleName = "Менеджер";
            string accountantRoleName = "Бухгалтер";
            string scepProRoleName = "Специалист по исполнительной документации";
            string specMinRoleName = "Специалист";



            Role directorRole = new Role { Id = 1, Name = directorRoleName };
            Role managerRole = new Role { Id = 2, Name = managerRoleName };
            Role accountantRole = new Role { Id = 3, Name = accountantRoleName };
            Role scepProRole = new Role { Id = 4, Name = scepProRoleName };
            Role specMinRole = new Role { Id = 5, Name = specMinRoleName };


            modelBuilder.Entity<Role>().HasData(new Role[] { directorRole, managerRole, accountantRole, scepProRole, specMinRole });
            base.OnModelCreating(modelBuilder);
        }
    }
}

