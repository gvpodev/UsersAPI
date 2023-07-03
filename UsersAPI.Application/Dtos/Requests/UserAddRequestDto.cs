using System.ComponentModel.DataAnnotations;

namespace UsersAPI.Application.Dtos.Requests
{
    public class UserAddRequestDto
    {
        [Required(ErrorMessage = "Please provide your name")]
        [MinLength(8, ErrorMessage = "Please provide a name with at least {1} characters")]
        [MaxLength(150, ErrorMessage = "Please provide a name with at most {1} characters")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Please provide the password")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@#$%^&+=])(?!.*\s).{8,}$",
            ErrorMessage = "Please provide some valid password")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Please provide the confirmation password")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@#$%^&+=])(?!.*\s).{8,}$",
            ErrorMessage = "Please provide some valid password")]
        [Compare("Password", ErrorMessage = "Passwords does not match")]
        public string? ConfirmationPassword { get; set; }

        [Required(ErrorMessage = "Please provide the e-mail address")]
        [EmailAddress(ErrorMessage = "Please provide some valid e-mail address")]
        public string? Email { get; set; }
    }
}
