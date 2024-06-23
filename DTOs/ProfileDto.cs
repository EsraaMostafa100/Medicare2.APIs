using System.ComponentModel.DataAnnotations;

namespace Medicare2.APIs.DTOs
{
    public class ProfileDto
    {
        [Required]
        public int Age { get; set; }
        [Required]
        public string BloodType { get; set; }
        [Required]
        public string Allergies { get; set; }
        [Required]
        public int Userid { get; set; }
    }
}
