namespace TaskManagementAPI.Models.DTO
{
    public class RegisterDto
    {
        public required string FullName { get; set; }

        public required string Email { get; set; }

        public required string Password { get; set; }

        public string? Role { get; set; }
    }
}
