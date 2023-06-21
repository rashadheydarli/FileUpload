using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PurpleBuzz.Areas.Admin.ViewModels.ServiceComponent;
using PurpleBuzz.DAL;
using PurpleBuzz.Models;

namespace PurpleBuzz.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceComponentController : Controller
    {
        private readonly AppDbContext _context;
        public ServiceComponentController(AppDbContext context)
        {
            _context = context;
        }
        #region Index
        [HttpGet]
        public IActionResult Index()        //indiye qeder yaradilan service componentleri gormek ucun
        {
            var serviceComponents = _context.serviceComponents.OrderByDescending(sc => sc.Id).ToList();

            var model = new ServiceComponentIndexVM
            {
                ServiceComponents = serviceComponents
            };
            return View(model); 
        }
        #endregion
        [HttpGet]
        public IActionResult Create()   // create viewsunu ekrana acmaq ucun 
        {
            return View();
        }


        #region create  
        [HttpPost]
        public IActionResult Create(ServiceComponentCreateVM model)    
        {
            if (!ModelState.IsValid) return View();

            var serviceComponent = new ServiceComponent
            {
                Title = model.Title,
                Subtitle = model.Subtitle,
                Description = model.Description
            };

            var dbServiceComponent = _context.serviceComponents.FirstOrDefault(sc => !sc.IsDeleted);

            if(dbServiceComponent is not null)
            {
                dbServiceComponent.IsDeleted = true;
                _context.serviceComponents.Update(dbServiceComponent);
            }
            


            _context.serviceComponents.Add(serviceComponent);
            _context.SaveChanges();

            return RedirectToAction("details", "servicecomponent", new {id = serviceComponent.Id});
        }
        #endregion

        [HttpGet]
        public IActionResult Details(int id)
        {
            var serviceComponent = _context.serviceComponents.Find(id);

            var model = new ServiceComponentDetailsVM
            {
                Title = serviceComponent.Title,
                Description = serviceComponent.Description,
                Subtitle = serviceComponent.Subtitle
            };

            return View(model);
        }

    }
}

