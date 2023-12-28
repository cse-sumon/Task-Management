using Microsoft.AspNetCore.Identity;

namespace TaskManagementAPI.Models.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
