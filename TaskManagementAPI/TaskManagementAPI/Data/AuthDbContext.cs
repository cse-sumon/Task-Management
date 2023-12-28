using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Helpers;
using TaskManagementAPI.Models.Domain;

namespace TaskManagementAPI.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            // Create Admin and User Role
            var adminRoleId = "28d65a5b-a7db-4850-b380-83591f7d7531";
            var userRoleId = "9740f16c-24a1-4224-a7be-1bb00b7c6892";
           
            var roles = new List<IdentityRole>()
            {
                new IdentityRole()
                { 
                    Id = adminRoleId,
                    Name = RolesName.Admin,
                    NormalizedName = RolesName.Admin.ToUpper(),
                    ConcurrencyStamp = adminRoleId

                },
                new IdentityRole()
                {
                    Id = userRoleId,
                    Name = RolesName.User,
                    NormalizedName = RolesName.User.ToUpper(),
                    ConcurrencyStamp = userRoleId

                }

            };

            // Seed the roles
            builder.Entity<IdentityRole>().HasData(roles);



            // Create an Admin User
            var adminUserId = "edc267ec-d43c-4e3b-8108-a1a1f819906d";

            var admin = new ApplicationUser()
            {
                Id = adminUserId,
                FullName = "Admin",
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                NormalizedEmail = "admin@gmail.com".ToUpper(),
                NormalizedUserName = "admin@gmail.com".ToUpper()
            };

            admin.PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(admin, "Admin@123");

            builder.Entity<ApplicationUser>().HasData(admin);

            // Set Roles to Admin

            var adminRoles = new List<IdentityUserRole<string>>()
            {
                new()
                {
                    UserId = adminUserId,
                    RoleId = adminRoleId
                },
                new()
                {
                    UserId = adminUserId,
                    RoleId = userRoleId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);


        }


    }
}
