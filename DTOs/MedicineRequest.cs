using System.ComponentModel.DataAnnotations;

namespace Medicare2.APIs.DTOs
{
    public class MedicineRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Dose { get; set; }
        [Required]
        public string Startdate { get; set; }
        [Required]
        public string Frequancy { get; set; }
        [Required]
        public string Form { get; set; }
        [Required]
        public string Duration { get; set; }
        [Required]
        public string Time { get; set; }
        public int UserId { get; set; }
    }
}
