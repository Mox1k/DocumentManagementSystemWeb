using DocumentManagementSystem.Data.Entites;

namespace DocumentManagementSystem.Models
{
    public class DocumentPageModel
    {
        public DocumentModel Model1 { get; set; }

        public List<Document> Model2 { get; set; } = new List<Document>();


    }
}
