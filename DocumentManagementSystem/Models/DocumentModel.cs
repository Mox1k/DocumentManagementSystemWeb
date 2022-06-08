using System.ComponentModel.DataAnnotations;

namespace DocumentManagementSystem.Models
{
    public class DocumentModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string? Text { get; set; }
    }
}
