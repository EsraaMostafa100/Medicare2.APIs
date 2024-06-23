using System.ComponentModel.DataAnnotations;

namespace Medicare2.APIs.DTOs
{
    public class UserRegisterRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")]
        public string Password { get; set; }
        [Required]
        [Phone]
        public string Phonenumber { get; set; }
        [Required]
        public string Dateofbirth { get; set; }
    }
}
