using Microsoft.EntityFrameworkCore;
using AppTemplate.Domain.Entities.Admin;
using AppTemplate.Domain.Entities.Settings;
using AppTemplate.Domain.Utilities;
using System.Security.Cryptography;

namespace AppTemplate.Domain.DBContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Admin
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<RoleMenuPermission> RoleMenuPermissions { get; set; }

        // Settings
        public DbSet<Department> Departments { get; set; }
        public DbSet<Designation> Designations { get; set; }
                

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            _ = SeedAsync(builder);
        }
        private async Task SeedAsync(ModelBuilder builder)
        {
            var securityStamp = Guid.NewGuid().ToString();
            var passwordHash = PasswordHasher.HashPassword("Admin@123", securityStamp);

            // Add your user creation logic here
            await Database.ExecuteSqlRawAsync("INSERT INTO public.\"Departments\"(\"Name\",\"CreatedAt\",\"StatusId\") VALUES('Software Engineering',CURRENT_TIMESTAMP,1);");
            await Database.ExecuteSqlRawAsync("INSERT INTO public.\"Designations\"(\"Name\",\"CreatedAt\",\"StatusId\") VALUES('Software Engineer',CURRENT_TIMESTAMP,1);");
            await Database.ExecuteSqlRawAsync("INSERT INTO public.\"UserTypes\"(\"Name\",\"CreatedAt\",\"StatusId\") VALUES('Admin',CURRENT_TIMESTAMP,1);");
            await Database.ExecuteSqlRawAsync("INSERT INTO public.\"Users\"(\"FullName\",\"Username\",\"Password\",\"SecurityStamp\",\"UserTypeId\",\"DepartmentId\",\"DesignationId\",\"CreatedAt\",\"StatusId\") VALUES('Admin','admin','" + passwordHash + "','" + securityStamp + "',1,1,1,CURRENT_TIMESTAMP,1);");
        }
    }
}
