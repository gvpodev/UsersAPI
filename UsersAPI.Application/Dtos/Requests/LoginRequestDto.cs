using System.ComponentModel.DataAnnotations;

namespace UsersAPI.Application.Dtos.Requests
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage = "Please provide the e-mail address")]
        [EmailAddress(ErrorMessage = "Please provide some valid e-mail address")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Please provide the password")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@#$%^&+=])(?!.*\s).{8,}$",
            ErrorMessage = "Please provide some valid password")]
        public string? Password { get; set; }
    }
}
