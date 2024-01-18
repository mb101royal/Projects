using Cars_automobile.Enums;
using Cars_automobile.Models;
using Cars_automobile.ViewModels.AuthViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cars_automobile.Controllers
{
    public class AuthController : Controller
    {
        readonly UserManager<AppUser> _userManager;
        readonly RoleManager<IdentityRole> _roleManager;
        readonly SignInManager<AppUser> _signInManager;

        public AuthController(UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        // Register

        // Get
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            /*await CreateRolesAndUsers();*/

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var findUserByEmail = await _userManager.FindByEmailAsync(vm.Email);

            if (findUserByEmail != null)
            {
                ModelState.AddModelError("Email", "This user is already registered");
                return View(vm);
            }
            var findUserByUsername = await _userManager.FindByNameAsync(vm.Username);

            if (findUserByUsername != null)
            {
            }

            if (await _userManager.Users.AnyAsync(u => u.UserName == vm.Username || u.Email == vm.Email))
            {
                ModelState.AddModelError("Username", "This user is already registered");
                return View(vm);
            }

            AppUser newUser = new()
            {
                UserName = vm.Username,
                Email = vm.Email,
                Fullname = vm.Fullname
            };

            await _userManager.CreateAsync(newUser, vm.Password);

            return RedirectToAction("Index", "Home");
        }

        // Login

        //Get
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Post
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            AppUser user;

            if (vm.UsernameOrEmail.Contains('@'))
            {
                user = await _userManager.FindByEmailAsync(vm.UsernameOrEmail);

                if (user == null)
                {
                    ModelState.AddModelError("UsernameOrEmail", "This user is not exist.");
                    return View(vm);
                }
            }
            else
            {
                user = await _userManager.FindByNameAsync(vm.UsernameOrEmail);

                if (user == null)
                {
                    ModelState.AddModelError("UsernameOrEmail", "This user is not exist.");
                    return View(vm);
                }
            }

            var loginResult = await _signInManager.CheckPasswordSignInAsync(user, vm.Password, true);

            if (loginResult.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            if (loginResult.IsLockedOut)
            {
                return RedirectToPage("./Lockout");
            }

            return RedirectToAction(nameof(Register));
        }

        async Task CreateRolesAndUsers()
        {
            bool roleExistResult = await _roleManager.RoleExistsAsync(Enum.GetName(Roles.GAdmin));
            if (!roleExistResult)
            {
                // First we create Admin role    

                var role = new IdentityRole();
                role.Name = "Admin";
                await _roleManager.CreateAsync(role);

                // Create Admin super user who will maintain the website                   

                var user = new AppUser();
                user.UserName = "GAdmin";
                user.Email = "GAdmin@admin.com";
                user.Fullname = "GAdmin Adminovich";

                string userPassword = "Admin123";

                IdentityResult checkUser = await _userManager.CreateAsync(user, userPassword);

                // Add default User role to Admin role
                if (checkUser.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Enum.GetName(Roles.GAdmin));
                }
            }

            // Creating Manager role    
            roleExistResult = await _roleManager.RoleExistsAsync(Enum.GetName(Roles.Admin));
            if (!roleExistResult)
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                await _roleManager.CreateAsync(role);
            }

            // Creating Moderator role     
            roleExistResult = await _roleManager.RoleExistsAsync(Enum.GetName(Roles.Moderator));
            if (!roleExistResult)
            {
                var role = new IdentityRole();
                role.Name = "Moderator";
                await _roleManager.CreateAsync(role);
            }

            // Creating Member role     
            roleExistResult = await _roleManager.RoleExistsAsync(Enum.GetName(Roles.Member));
            if (!roleExistResult)
            {
                var role = new IdentityRole();
                role.Name = "Member";
                await _roleManager.CreateAsync(role);
            }
        }

    }
}
