using System.ComponentModel.DataAnnotations;

namespace UsersAPI.Application.Dtos.Requests
{
    public class ForgotPasswordRequestDto
    {
        [Required(ErrorMessage = "Please provide the password")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@#$%^&+=])(?!.*\s).{8,}$",
            ErrorMessage = "Please provide some valid password")]
        public string? Email { get; set; }
    }
}