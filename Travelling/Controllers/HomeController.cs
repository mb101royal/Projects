using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Travelling.Contexts;
using Travelling.Models;

namespace Travelling.Controllers
{
    public class HomeController : Controller
    {

        public HomeController(TravellingDbContext context)
        {
            _context = context;
        }

        TravellingDbContext _context { get; }

        public IActionResult Index()
        {
            var destinations = _context.Destinations;
            return View(destinations);
        }

    }
}
