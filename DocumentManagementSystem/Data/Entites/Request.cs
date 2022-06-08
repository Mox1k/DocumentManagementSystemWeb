namespace DocumentManagementSystem.Data.Entites
{
    public class Request
    {
        public int Id { get; set; }
        public string NumberReq { get; set; }
        public DateTime DateReq { get; set; }
        public string NameReq { get; set; }
        public string TextReq { get; set; }
        public Client? Client { get; set; }
        public int ClientId { get; set; }
    }
}
