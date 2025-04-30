namespace College2Career.DTO
{
    public class CompaniesDTO
    {
        public int companyId { get; set; }
        public string? companyName { get; set; }
        public string? establishedDate { get; set; }
        public string? contactNumber { get; set; }
        public IFormFile? profilePicture { get; set; }
        public string? profilePictureURL { get; set; }
        public string? industry { get; set; }
        public string? area { get; set; }
        public string? city { get; set; }
        public string? state { get; set; }
        public string? employeeSize { get; set; }
        public string? reasonOfStatus { get; set; }
        public string? status { get; set; }
        public int? usersId { get; set; }
        public DateTime createdAt { get; set; }
    }
}
