namespace DocumentManagementSystem.Models
{
    public class RequestModel
    {
        public string NumberReq { get; set; }
        public DateTime DateReq { get; set; }
        public string NameReq { get; set; }
        public string TextReq { get; set; }
        public int ClientId { get; set; }
    }
}
