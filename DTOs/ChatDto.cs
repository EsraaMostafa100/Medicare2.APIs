using System.ComponentModel.DataAnnotations;

namespace Medicare2.APIs.DTOs
{
    public class ChatDto
    {
        [Required]
        public string message { get; set; }
        [Required]
        public int Userid { get; set; }
        [Required]
        public int Doctorid { get; set; }
    }
}
