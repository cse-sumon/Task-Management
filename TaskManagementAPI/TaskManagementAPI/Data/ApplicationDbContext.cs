using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Models.Domain;


namespace TaskManagementAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }


 
        public DbSet<TaskModel> Tasks { get; set; }







    }
}
