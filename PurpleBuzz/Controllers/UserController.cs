using Microsoft.AspNetCore.Mvc;
using PurpleBuzz.DAL;
using PurpleBuzz.Models;
using PurpleBuzz.ViewModels.User;

namespace PurpleBuzz.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;

        }

        [HttpGet]
        public IActionResult Create()
        {
            
            return View();
        }

        [HttpPost]

        public IActionResult Create(UserCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = new User
            {
                Name = model.Name,
                SurName = model.Surname

            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(user);
        }

    }
}
