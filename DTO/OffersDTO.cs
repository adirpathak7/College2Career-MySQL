namespace College2Career.DTO
{
    public class OffersDTO
    {
        public string? annualPackage { get; set; }
        public string? joiningDate { get; set; }
        public string? timing { get; set; }
        public string? position { get; set; }
        public string? description { get; set; }
        public IFormFile? offerLetter { get; set; }
        public string? offerLetterURL { get; set; }
        public string? status { get; set; }
        public string? reason { get; set; } = null;
        public int? applicationId { get; set; }
        public DateTime? createdAt { get; set; }
        public string? companyName { get; set; }
        public string? studentName { get; set; }
    }
}
