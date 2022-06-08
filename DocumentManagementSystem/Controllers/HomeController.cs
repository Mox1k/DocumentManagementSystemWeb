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
        private RequestPageModel _requestModel = new RequestPageModel();
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
            var doc = new Document()
            {
                Name = document.Model1.Name,
                Date = document.Model1.Date,
                Text = document.Model1.Text
            };
            _context.Documents.Add(doc);

            await _context.SaveChangesAsync();
            return RedirectToAction("Documents", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> Documents()
        {
            var documents = await _context.Documents.ToListAsync();

            _doucmentModel.Model2 = documents;

            return View(_doucmentModel);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteDocument(int Id)
        {

            if (Id != null)
            {
                var document = await _context.Documents.FindAsync(Id);
                if (document != null)
                    _context.Remove(document);

                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Documents", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> CreateRequest(RequestPageModel request)
        {
            var req = new Request()
            {
                NameReq = request.Model1.NameReq,
                DateReq = request.Model1.DateReq,
                NumberReq = request.Model1.NumberReq,
                TextReq = request.Model1.TextReq,
                ClientId = request.Model1.ClientId,
            };
            _context.Requests.Add(req);
            await _context.SaveChangesAsync();

            return RedirectToAction("Requests", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> Requests(RequestPageModel request)
        {
            var clients = await _context.Clients.ToListAsync();
            _requestModel.Model2 = clients;
            var req = await _context.Requests.Include(x => x.Client).ToListAsync();
            _requestModel.Model3 = req;
            return View(_requestModel);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteRequest(int Id)
        {

            if (Id != null)
            {
                var req = await _context.Requests.FindAsync(Id);

                if (req != null)
                    _context.Remove(req);

                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Requests", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> DocumentShow(int Id)
        {
            var doc = await _context.Documents.FindAsync(Id);
            DocumentShowModel document = new DocumentShowModel()
            {
                Text = doc.Text,
            };

            return View(document);
        }
    }
}
