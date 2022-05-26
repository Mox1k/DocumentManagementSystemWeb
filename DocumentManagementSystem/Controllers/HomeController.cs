using DocumentManagementSystem.Data.Contexts;
using DocumentManagementSystem.Data.Entites;
using DocumentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DocumentManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private ClientPageModel _pageModel = new ClientPageModel();
        public HomeController(ApplicationDbContext context)
        {
            _context = context;

        }
        [Authorize]
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
    }
}
