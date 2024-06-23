namespace Medicare2.APIs.DTOs
{
    public class PharmacyToReturnDTo
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string Services { get; set; }
        public bool IsSaved { get; set; }
        public decimal Rate { get; set; }
        public int Userid { get; set; }
    }
}
