using System;

namespace EmployeeManagment.API.DTO
{
    public class SignupDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class AuthResponseDTO
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
        public class RegisterUserDTO
        {
            public string Username { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public DateTime? DateOfBirth { get; set; }
        }

        public class LoginUserDTO
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
}

