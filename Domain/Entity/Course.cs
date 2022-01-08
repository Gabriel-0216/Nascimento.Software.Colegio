namespace Domain.Entity
{
    public class Course
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public string Resume { get; set; } = string.Empty;
        public DateTime Created_Date { get; set; } = DateTime.Now;
        public DateTime Updated_At { get; set; }
    }
}
