using System.ComponentModel.DataAnnotations;

namespace DocumentManagementSystem.Data.Entites
{
    public class Document
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public User? User { get; set; }
        public int? UserId { get; set; }
    }
}
