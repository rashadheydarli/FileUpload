using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PurpleBuzz.DAL;
using PurpleBuzz.ViewModels.Work;

namespace PurpleBuzz.Controllers
{
    public class WorkController : Controller
    {
        private readonly AppDbContext _context;

        public WorkController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
           

            var model = new WorkIndexVM
            {
                WorkCategories = _context.WorkCategories
                                        .Where(wc => !wc.IsDeleted)
                                        .Include(x => x.Works.Where(w => !w.IsDeleted))
                                        .ToList(),
                FeaturedWorkComponent = _context.FeaturedWorkComponent.Include(fwc => fwc.Photos.OrderBy(p=>p.Order)).FirstOrDefault()
        };

            return View(model);
        }
    }
}
