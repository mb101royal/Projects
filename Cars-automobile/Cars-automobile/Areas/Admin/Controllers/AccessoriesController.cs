using Cars_automobile.Contexts;
using Cars_automobile.Helpers;
using Cars_automobile.Models;
using Cars_automobile.ViewModels.AccessoryViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace Cars_automobile.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccessoriesController : Controller
    {
        readonly CarsAutomobileDbContext _context;

        public AccessoriesController(CarsAutomobileDbContext context)
        {
            _context = context;
        }

        // Index
        public async Task<IActionResult> Index()
        {
            var accessoriesFromDb = await _context.Accessories.Select(t => new AccessoryDetailsViewModel
            {
                Description = t.Description,
                Image = t.Image,
                Title = t.Title,
            }).ToListAsync();

            return View(accessoriesFromDb);
        }

        // Create

        // Get
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Post
        [HttpPost]
        public async Task<IActionResult> Create(AccessoryCreateViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            if (vm.ImageFile != null)
            {
                if (!vm.ImageFile.IsCorrectType())
                {
                    ModelState.AddModelError("Image", "Incorrect file type");
                    return View(vm);
                }
            }

            if (!vm.ImageFile.IsCorrectSize(300))
            {
                ModelState.AddModelError("ImageFile", $"Size of the image must be less than {300} kb");
                return View(vm);
            }

            Accessory newAccessory = new()
            {
                Title = vm.Title,
                Description = vm.Description,
                Image = vm.ImageFile?.SaveImageFileAsync(PathConstants.ImagesLocation).Result ?? "",
            };

            await _context.AddAsync(newAccessory);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Edit

        // Get
        [HttpGet]
        public async Task<IActionResult> Edit(Guid? guId)
        {
            if (Common.CheckGuId(guId)) return BadRequest();

            var accessoryFromDb = await _context.Accessories.FindAsync(guId) ?? throw new Exception();

            AccessoryEditViewModel vm = new()
            {
                Title = accessoryFromDb.Title,
                Description = accessoryFromDb.Description,
            };

            return View(vm);
        }

        // Post
        [HttpPost]
        public async Task<IActionResult> Edit(Guid? guId, AccessoryEditViewModel vm)
        {
            if (Common.CheckGuId(guId)) return BadRequest();

            if (!ModelState.IsValid) return View(vm);

            if (vm.ImageFile != null)
            {
                if (!vm.ImageFile.IsCorrectType())
                {
                    ModelState.AddModelError("Image", "Incorrect file type");
                    return View(vm);
                }
            }

            var accessoryFromDb = await _context.Accessories.FirstOrDefaultAsync(t => t.Id == guId) ?? throw new Exception();

            accessoryFromDb.Title = vm.Title;
            accessoryFromDb.Description = vm.Description;
            accessoryFromDb.Image = vm.ImageFile?.SaveImageFileAsync(PathConstants.ImagesLocation).Result ?? "";

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid? guId)
        {
            if (Common.CheckGuId(guId)) return BadRequest();

            var accessoryFromDb = await _context.Accessories.FindAsync(guId) ?? throw new Exception();

            _context.Remove(accessoryFromDb);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
