namespace College2Career.DTO
{
    public class CollegesDTO
    {
        public int collegeId { get; set; }
        public string collegeName { get; set; }
        public string establishedDate { get; set; }
        public string contactNumber { get; set; }
        public FormFile profilePicture { get; set; }
        public string area { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public DateTime createdAt { get; set; }
    }
}
