using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PurpleBuzz.Areas.Admin.ViewModels.WorkCategory;
using PurpleBuzz.DAL;
using PurpleBuzz.Models;

namespace PurpleBuzz.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WorkCategoryController : Controller
    {
        private readonly AppDbContext _context;
        public WorkCategoryController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var model = new WorkCategoryIndexVM
            {
                WorkCategories = _context.WorkCategories.Where(wc => !wc.IsDeleted).ToList()
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(WorkCategoryCreateVM model)
        {
            if (!ModelState.IsValid) return View();
            var workCategory = _context.WorkCategories.FirstOrDefault(wc => wc.Name.Trim().ToLower() == model.Name.Trim().ToLower() && !wc.IsDeleted);
            if(workCategory is not null)
            {
                ModelState.AddModelError("Name", "Bu adda kategoriya mövcuddur");
                return View();
            }

            workCategory = new WorkCategory
            {
                Name = model.Name,
                CreatedDate = DateTime.Now
            };

            _context.WorkCategories.Add(workCategory);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Update(int id)
        {
            var workCategory = _context.WorkCategories.FirstOrDefault(wc => wc.Id == id && !wc.IsDeleted);
            if (workCategory is null) return NotFound();
            var model = new WorkCategoryUpdateVM
            {
                Name = workCategory.Name

            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Update(int id, WorkCategoryUpdateVM model)
        {
            if (!ModelState.IsValid) return View();

            var workCategory = _context.WorkCategories.FirstOrDefault(wc => wc.Name.Trim().ToLower() == model.Name.Trim().ToLower() && wc.Id != id && !wc.IsDeleted);
            if(workCategory is not null)
            {
                ModelState.AddModelError("Name", "Bu adda kategoriya mövcuddur");
                return View(); 
            }


            workCategory = _context.WorkCategories.FirstOrDefault(wc => wc.Id == id && !wc.IsDeleted);
            if (workCategory is null) return NotFound();

            workCategory.Name = model.Name;
            workCategory.ModifiedDate = DateTime.Now;
            _context.WorkCategories.Update(workCategory);
            _context.SaveChanges();

            return RedirectToAction("index");
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var workCategory = _context.WorkCategories.FirstOrDefault(wc => wc.Id == id);
            if (workCategory is null) return NotFound();


            workCategory.IsDeleted = true;
            workCategory.DeletedDate = DateTime.Now;

            _context.WorkCategories.Update(workCategory);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var workCategory = _context.WorkCategories.Include(wc => wc.Works.Where(w => !w.IsDeleted)).FirstOrDefault(wc => wc.Id == id && !wc.IsDeleted);
            if (workCategory is null) return NotFound();

            var model = new WorkCategoryDetailsVM
            {
                Name = workCategory.Name,
                IsDeleted = workCategory.IsDeleted,
                CreatedDate = workCategory.CreatedDate,
                ModifiedDate =workCategory.ModifiedDate,
                DeletedDate = workCategory.DeletedDate,
                Works = workCategory.Works.ToList()
            };

            return View(model);
        }
    }
}

