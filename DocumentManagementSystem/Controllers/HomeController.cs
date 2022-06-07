using DocumentManagementSystem.Data.Contexts;
using DocumentManagementSystem.Data.Entites;
using DocumentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DocumentManagementSystem.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private ClientPageModel _pageModel = new ClientPageModel();
        private EmployeePageModel _employeeModel = new EmployeePageModel();
        private DocumentPageModel _doucmentModel = new DocumentPageModel();
        public HomeController(ApplicationDbContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Clients(string? name)
        {
            if (name == null || name == string.Empty)
            {
                var clients = await _context.Clients.ToListAsync();
                if (clients != null)
                {
                    _pageModel.Model1 = clients;
                }
            }
            else
            {
                var clients = await _context.Clients.Where(x => x.Name.Contains(name)).ToListAsync();
                if (clients != null)
                {
                    _pageModel.Model1 = clients;
                }
            }

            return View(_pageModel);
        }
        [HttpPost]
        public async Task<IActionResult> Clients(ClientPageModel model)
        {
            _context.Add(model.Model2);
            await _context.SaveChangesAsync();
            return RedirectToAction("Clients", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteClients(string code)
        {
            if (code != null)
            {
                var client = await _context.Clients.Where(x => x.Code == code).FirstOrDefaultAsync();

                if (client != null)
                {
                    _context.Clients.Remove(client);
                }

                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Clients", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> Employees(int? Id)
        {
            var roles = await _context.Roles.ToListAsync();

            _employeeModel.Model2 = roles;

            if (Id == null)
            {
                var users = await _context.Users.Include(x => x.Role).ToListAsync();
                foreach (var user in users)
                {
                    _employeeModel.Model1.Add(new EmployeeModel()
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Surname = user.Surname,
                        Patronymic = user.Patronymic,
                        Role = user.Role.Name.ToString()
                    });
                }
            }
            else
            {
                var user = await _context.Users.Include(x => x.Role).Where(x => x.Id == Id).FirstOrDefaultAsync();
                if (user != null)
                {
                    _employeeModel.Model1.Add(new EmployeeModel()
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Patronymic = user.Patronymic,
                        Surname = user.Surname,
                        Role = user.Role.Name
                    });
                }
            }
            return View(_employeeModel);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteEmployee(int? Id)
        {
            if (Id != null)
            {
                var user = await _context.Users.FindAsync(Id);
                if (user != null)
                {
                    _context.Users.Remove(user);
                }
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Employees", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> ChangeRoleEmployee(int? RoleId, int? Id)
        {
            if (RoleId != null && Id != null)
            {
                var user = await _context.Users.FindAsync(Id);
                var role = await _context.Roles.Where(x => x.Id == RoleId).FirstOrDefaultAsync();

                if (user != null && role != null)
                {
                    user.RoleId = role.Id;
                    _context.Users.Update(user);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction("Employees", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> CreateDocument(DocumentPageModel document)
        {
            if (document.Model1 != null)
            {
                var doc = new Document()
                {
                    Number = document.Model1.Number,
                    Date = document.Model1.Date,
                    Text = document.Model1.Text
                };
                _context.Documents.Add(doc);
            }
            return RedirectToAction("Documents", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> Documents()
        {
            return View();
        }
    }
}
