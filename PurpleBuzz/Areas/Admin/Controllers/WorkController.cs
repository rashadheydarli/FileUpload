using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PurpleBuzz.Areas.Admin.ViewModels.Work;
using PurpleBuzz.DAL;
using PurpleBuzz.Models;
using WorkIndexVM = PurpleBuzz.Areas.Admin.ViewModels.Work.WorkIndexVM;

namespace PurpleBuzz.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class WorkController : Controller
    {
        private readonly AppDbContext _context;
        public WorkController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var model = new WorkIndexVM
            {
               Works = _context.Works.Include(w => w.WorkCategory).Where(w => !w.IsDeleted).ToList()
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new WorkCreateVM
            {
               WorkCategories = _context.WorkCategories.Where(wc => !wc.IsDeleted).Select( wc => new SelectListItem
               {
                   Text = wc.Name,
                   Value = wc.Id.ToString(),
               }).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(WorkCreateVM model)
        {
            model.WorkCategories = _context.WorkCategories.Where(wc => !wc.IsDeleted).Select(wc => new SelectListItem
            {
                Text = wc.Name,
                Value = wc.Id.ToString(),
            }).ToList();
            if (!ModelState.IsValid) return View(model);
            

            var work = _context.Works.FirstOrDefault(w => w.Title.Trim().ToLower() == model.Title.Trim().ToLower() &&
                                                            !w.IsDeleted);
            if(work is not null)
            {
                ModelState.AddModelError("Title", "Bu adda iş mövcuddur");
                return View(model);
            }

            var workCategory = _context.WorkCategories.FirstOrDefault(wc => wc.Id == model.WorkCategoryId &&
                                                                            !wc.IsDeleted);
            if(workCategory is null)
            {
                ModelState.AddModelError("WorkCategoryId", "Kateqoriya mövcud deyil");
                return View(model);
            }
           
            

            work = new Work
            {
                Title = model.Title,
                Description = model.Description,
                PhotoPath = model.PhotoPath,
                WorkCategoryId = workCategory.Id,
                CreatedDate = DateTime.Now

            };

            _context.Works.Add(work);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        #region Update
        [HttpGet]
        public IActionResult Update(int id)
        {
            var work = _context.Works.FirstOrDefault(w => w.Id == id && !w.IsDeleted);
            if (work is null) NotFound();

            var model = new WorkUpdateVM
            {
                Title = work.Title,
                Description = work.Description,
                PhotoPath = work.PhotoPath,
                WorkCategories = _context.WorkCategories.Where(wc => !wc.IsDeleted).Select(wc => new SelectListItem
                {
                    Text = wc.Name,
                    Value = wc.Id.ToString()
                }).ToList(),
                WorkCategoryId = work.WorkCategoryId
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Update(int id, WorkUpdateVM model)
        {
            model.WorkCategories = _context.WorkCategories.Where(wc => !wc.IsDeleted).Select(wc => new SelectListItem
            {
                Text = wc.Name,
                Value = wc.Id.ToString(),
            }).ToList();
            if (!ModelState.IsValid) return View(model);
            

            var work = _context.Works.FirstOrDefault(w => w.Title.Trim().ToLower() == model.Title.Trim().ToLower() &&
                                                            w.Id != id &&
                                                            !w.IsDeleted);
            if (work is not null)
            {
                ModelState.AddModelError("Title", "Bu adda iş mövcuddur");
                return View(model);
            }


            work = _context.Works.FirstOrDefault(w => w.Id == id && !w.IsDeleted);
            if (work is null) return NotFound();

            var workCategory = _context.WorkCategories.FirstOrDefault(wc => wc.Id == model.WorkCategoryId &&
                                                                            !wc.IsDeleted);
            if(workCategory is null)
            {
                ModelState.AddModelError("WorkCategoryId", "Belə bir kateqoriya yoxdur");
                return View(model);
            }


            work.Title = model.Title;
            work.Description = model.Description;
            work.PhotoPath = model.PhotoPath;
            work.ModifiedDate = DateTime.Now;

            _context.Works.Update(work);
            _context.SaveChanges();

            return RedirectToAction("index");
        }
        #endregion


        #region Details
        public IActionResult Details(int Id)
        {
            var work = _context.Works.Include(w => w.WorkCategory).FirstOrDefault(w => w.Id == Id && !w.IsDeleted);

            if (work is null) return NotFound();

            var model = new WorkDetailsVM
            {
                Title = work.Title,
                Description = work.Description,
                CreatedDate = work.CreatedDate,
                ModifiedDate = work.ModifiedDate,
                PhotoPath = work.PhotoPath,
                WorkCategory = work.WorkCategory
            };
            return View(model);

            
           
        }

        #endregion

        #region Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var work = _context.Works.FirstOrDefault(w => w.Id == id && !w.IsDeleted);
            if (work is null) NotFound();

            work.IsDeleted = true;
            work.DeleteDate = DateTime.Now;

            _context.Works.Update(work);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        #endregion
    }
}

