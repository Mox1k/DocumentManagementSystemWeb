namespace DocumentManagementSystem.Data.Entites
{
    public class Document
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
