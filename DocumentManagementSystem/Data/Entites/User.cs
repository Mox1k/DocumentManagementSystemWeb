namespace DocumentManagementSystem.Data.Entites
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime Created { get; set; }   

        public Guid? RoleId { get; set; }
        public Role Role { get; set; }
    }
}
