using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Travelling.Contexts;
using Travelling.Models;
using Travelling.ViewModels.DestinationViewModel;

namespace Travelling.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DestinationsController : Controller
    {

        TravellingDbContext _context { get; }

        public DestinationsController(TravellingDbContext context)
        {
            _context = context;
        }

        // Index:

        public async Task<IActionResult> Index()
        {
            var destinationsFromDb = await _context.Destinations.Select(destination => new DestinationListItemViewModel
            {
                Id = destination.Id,
                ImageUrl = destination.ImageUrl,
                Price = destination.Price,
                Rating = destination.Rating,
                Title = destination.Title
            }).ToListAsync();

            return View(destinationsFromDb);
        }

        // Create:

        // Get
        public IActionResult Create()
        {
            return View();
        }

        // Post
        [HttpPost]
        public async Task<IActionResult> Create(DestinationCreateViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            Destination destination = new Destination
            {
                Title = vm.Title,
                ImageUrl = vm.ImageUrl,
                Price = vm.Price,
                Rating = vm.Rating,
            };

            await _context.AddAsync(destination);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Udpate:

        // Get
        public async Task<IActionResult> Update(int? id, DestinationUpdateViewModel vm)
        {
            if (id == null || id <= 0) return BadRequest();

            var destinationFromDb = await _context.Destinations.FindAsync(id);

            if (destinationFromDb == null) return NotFound();

            destinationFromDb.Title = vm.Title;
            destinationFromDb.Price = vm.Price;
            destinationFromDb.Rating = vm.Rating;
            destinationFromDb.ImageUrl = vm.ImageUrl;

            return View(vm);
        }

        // Post
        [HttpPost]
        public async Task<IActionResult> Update(DestinationUpdateViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var destinationToUpdate = new Destination
            {
                Title = vm.Title,
                ImageUrl = vm.ImageUrl,
                Price = vm.Price,
                Rating = vm.Rating
            };

            await _context.AddAsync(destinationToUpdate);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Delete:

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id <= 0) return BadRequest();

            var destinationFromDb = await _context.Destinations.FindAsync(id);

            if (destinationFromDb == null) return NotFound();

            _context.Destinations.Remove(destinationFromDb);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
