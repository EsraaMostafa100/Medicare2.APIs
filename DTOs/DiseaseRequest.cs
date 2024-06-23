using System.ComponentModel.DataAnnotations;

namespace Medicare2.APIs.DTOs
{
    public class DiseaseRequest
    {
        [Required]
        public string Name { get; set; }
        public int UserId { get; set; }
    }
}
