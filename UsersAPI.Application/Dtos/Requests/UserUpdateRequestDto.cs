using System.ComponentModel.DataAnnotations;

namespace UsersAPI.Application.Dtos.Requests
{
    public class UserUpdateRequestDto
    {
        [Required(ErrorMessage = "Please provide your name")]
        [MinLength(8, ErrorMessage = "Please provide a name with at least {1} characters")]
        [MaxLength(150, ErrorMessage = "Please provide a name with at most {1} characters")]
        public string? Name { get; set; }
    }
}
