using Cars_automobile.Contexts;
using Cars_automobile.Helpers;
using Cars_automobile.Models;
using Cars_automobile.ViewModels;
using Cars_automobile.ViewModels.AccessoryViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Cars_automobile.Controllers
{
    public class HomeController : Controller
    {
        readonly CarsAutomobileDbContext _context;

        public HomeController(CarsAutomobileDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            HomeViewModel vm = new()
            {
                Accessories = await _context.Accessories.Select(t => new AccessoryDetailsViewModel
                {
                    Title = t.Title,
                    Description = t.Description,
                    Image = Path.Combine(PathConstants.ImagesLocation, t.Image),
                }).ToListAsync()
            };
            return View(vm);
        }
    }
}
