using DocumentManagementSystem.Data.Entites;

namespace DocumentManagementSystem.Models
{
    public class RequestPageModel
    {
        public RequestModel Model1 { get; set; } 
        public List<Client> Model2 { get; set; } = new List<Client>();
    }
}
