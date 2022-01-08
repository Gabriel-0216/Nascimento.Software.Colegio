namespace Domain.Entity
{
    public class Student
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime Birthdate { get; set; }
        public DateTime Create_Date { get; set; }
        public DateTime Updated_Date { get; set; }

    }
}
