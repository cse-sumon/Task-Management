﻿namespace TaskManagementAPI.Models.DTO
{
    public class LoginResponseDto
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public string Token { get; set; }

        public List<string> Roles { get; set; }
    }
}
