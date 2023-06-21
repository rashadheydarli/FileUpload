using Microsoft.AspNetCore.Mvc;
using PurpleBuzz.DAL;
using PurpleBuzz.Models;
using PurpleBuzz.ViewModels.Home;

namespace PurpleBuzz.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
            
        public IActionResult Index()
        {
            var serviceComponent = _context.serviceComponents.FirstOrDefault(sc => !sc.IsDeleted);

            var model = new HomeIndexVM
            {
                ServiceComponent = serviceComponent,
                Works = _context.Works.OrderByDescending(w => w.Id).Take(3).ToList()
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult LoadMore(int skipRow)
        {
            var model = new HomeLoadMoreVM
            {
                Works = _context.Works.OrderByDescending(w => w.Id).Skip(3*skipRow).Take(3).ToList()
            };
            
            return PartialView("_RecentWorkComponentPartial", model);
        }
    }
}
