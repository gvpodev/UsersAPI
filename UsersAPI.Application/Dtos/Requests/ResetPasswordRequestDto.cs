using System.ComponentModel.DataAnnotations;

namespace UsersAPI.Application.Dtos.Requests
{
    public class ResetPasswordRequestDto
    {
        [Required(ErrorMessage = "Please provide your current password")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@#$%^&+=])(?!.*\s).{8,}$",
            ErrorMessage = "Please provide some valid password")]
        public string? CurrentPassword { get; set; }

        [Required(ErrorMessage = "Please provide the new password")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@#$%^&+=])(?!.*\s).{8,}$",
            ErrorMessage = "Please provide some valid password")]
        public string? NewPassword { get; set; }

        [Required(ErrorMessage = "Please provide the confirmation password")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@#$%^&+=])(?!.*\s).{8,}$",
            ErrorMessage = "Please provide some valid password")]
        [Compare("NewPassword", ErrorMessage = "Passwords does not match")]
        public string? NewPasswordConfirmation { get; set; }
    }
}
