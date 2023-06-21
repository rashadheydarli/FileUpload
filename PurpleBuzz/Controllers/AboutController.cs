using Microsoft.AspNetCore.Mvc;
using PurpleBuzz.DAL;
using PurpleBuzz.ViewModels.About;

namespace PurpleBuzz.Controllers
{
    public class AboutController : Controller
    {
        private readonly AppDbContext _context;

        public AboutController(AppDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            var aboutIntroComponent = _context.AboutIntroComponents.FirstOrDefault(aic => !aic.IsDeleted);
            var whyYouChoose = _context.WhyYouChooses.FirstOrDefault(wyc => !wyc.IsDeleted);
            var ours = _context.Ours.Where(o => !o.IsDeleted).ToList();
            var model = new AboutIndexVM
            {
                WhyYouChoose = whyYouChoose,
                AboutIntroComponent = aboutIntroComponent,
                Ours = ours,
                TeamMembers = _context.TeamMembers.ToList()
            };

            return View(model);
        } 
    }
}
