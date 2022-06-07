using DocumentManagementSystem.Data.Entites;

namespace DocumentManagementSystem.Models
{
    public class EmployeePageModel
    {
        public IList<EmployeeModel> Model1 { get; set; } = new List<EmployeeModel>();
        public IList<Role> Model2 { get; set; } = new List<Role>();
    }
}
