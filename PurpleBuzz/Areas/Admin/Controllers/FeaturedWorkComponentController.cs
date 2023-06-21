using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PurpleBuzz.Areas.Admin.ViewModels.FeaturedWorkComponent;
using PurpleBuzz.DAL;
using PurpleBuzz.Models;
using PurpleBuzz.Utilities;

namespace PurpleBuzz.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/featured-work-component/{action=list}/{id?}")]
    public class FeaturedWorkComponentController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IFileService _fileService;
        public FeaturedWorkComponentController(AppDbContext context, IFileService fileService)
        {
            _context = context;      //birbasa obyekt yaratmadan muraciet etmek dependency injection
            _fileService = fileService;
        }
        [HttpGet]
        public IActionResult List()
        {
            var model = new FeaturedWorkComponentListVM
            {
                FeaturedWorkComponent = _context.FeaturedWorkComponent.FirstOrDefault()
                //eger photolari gosterseydim Include() edecekdim
            };
            return View(model);
        }
        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            var featuredWorkComponent = _context.FeaturedWorkComponent.FirstOrDefault();
            if (featuredWorkComponent is not null) return BadRequest();
            
            return View();
        }
        [HttpPost]
        public IActionResult Create(FeaturedWorkComponentCreateVM model)
        {
            if (!ModelState.IsValid) return View();

            foreach (var photo in model.Photos)
            {

                if (!_fileService.IsImage(photo))
                {
                    ModelState.AddModelError("Photos", "Fayl düzgün formatda deyil");
                    return View();
                }

                if (_fileService.IsBiggerThanSize(photo, 200))
                {
                    ModelState.AddModelError("Photos", "Şəkilin ölçüsü 200kb dan çoxdur");
                    return View();
                }
            }
            var featuredWorkComponent = new FeaturedWorkComponent
            {
                Description = model.Description,
                CreatedDate = DateTime.Now
            };

            _context.FeaturedWorkComponent.Add(featuredWorkComponent);
            //savechanges ona gore etmirik ki hem fw component hem de  fw photolar yaranmalidi

            int order = 1;
            foreach (var photo in model.Photos)
            {
                var featuredWorkComponentPhoto = new FeaturedWorkComponentPhoto
                {
                    Name = _fileService.Upload(photo),
                    Order = order++,
                    CreatedAt = DateTime.Now,
                    FeaturedWorkComponent = featuredWorkComponent
                };

                _context.FeaturedWorkComponentPhotos.Add(featuredWorkComponentPhoto);
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(List));

        }
        #endregion

        #region Details
        [HttpGet]
        public IActionResult Details(int id)
        {
            var featuredWorkComponent = _context.FeaturedWorkComponent.Include(fwc => fwc.Photos).FirstOrDefault(fwc => fwc.Id == id);
            if (featuredWorkComponent is null) return NotFound();

            var model = new FeaturedWorkComponentDetailsVM
            {
                Description = featuredWorkComponent.Description,
                CreateAt = featuredWorkComponent.CreatedDate,
                ModifiedAt = featuredWorkComponent.ModifiedDate,
                Photos = featuredWorkComponent.Photos.ToList()
            };
            return View(model);
        }

        #endregion

        #region Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var featuredWorkComponent = _context.FeaturedWorkComponent.Find(id);
            if (featuredWorkComponent is null) return NotFound();

            _context.FeaturedWorkComponent.Remove(featuredWorkComponent);
            _context.SaveChanges();

            return RedirectToAction(nameof(List));
        }
        #endregion

        #region Update
        [HttpGet]
        public IActionResult Update(int id)
        {
            var featuredWorkComponent = _context.FeaturedWorkComponent.Include(fwc => fwc.Photos).FirstOrDefault(fwc => fwc.Id == id);
            if (featuredWorkComponent is null) return NotFound();

            var model = new FeaturedWorkComponentUpdateVM
            {
                Description = featuredWorkComponent.Description,
                FeaturedWorkComponentPhotos = featuredWorkComponent.Photos.ToList()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Update(int id, FeaturedWorkComponentUpdateVM model)
        {
            if (!ModelState.IsValid) return View();

            var featuredWorkComponent = _context.FeaturedWorkComponent.Include(fwc => fwc.Photos).FirstOrDefault(fwc => fwc.Id == id);
            if (featuredWorkComponent is null) return NotFound();

            featuredWorkComponent.Description = model.Description;
            _context.Update(featuredWorkComponent);

            foreach (var photo in model.Photos)
            {

                if (!_fileService.IsImage(photo))
                {
                    ModelState.AddModelError("Photos", "Fayl düzgün formatda deyil");
                    return View();
                }

                if (_fileService.IsBiggerThanSize(photo, 200))
                {
                    ModelState.AddModelError("Photos", "Şəkilin ölçüsü 200kb dan çoxdur");
                    return View();
                }
            }

            var lastOrder = featuredWorkComponent.Photos.OrderByDescending(p => p.Order).FirstOrDefault()?.Order ?? 1;

            int order = 1;
            foreach (var photo in model.Photos)
            {
                var featuredWorkComponentPhoto = new FeaturedWorkComponentPhoto
                {
                    Name = _fileService.Upload(photo),
                    CreatedAt = DateTime.Now,
                    FeaturedWorkComponent = featuredWorkComponent,
                    Order = order++
                };
                _context.FeaturedWorkComponentPhotos.Add(featuredWorkComponentPhoto);
            }
            _context.SaveChanges();

            return RedirectToAction(nameof(Details), "featureworkcomponent", new { id = featuredWorkComponent.Id});
        }

        [HttpGet]
        public IActionResult UpdatePhoto(int id)
        {
            var featuredWorkComponentPhoto = _context.FeaturedWorkComponentPhotos.Find(id);
            if (featuredWorkComponentPhoto is null) return NotFound();

            var model = new FeaturedWorkComponentUpdatePhotoVM
            {
                Order = featuredWorkComponentPhoto.Order
            };
            return View(model);

        }
        [HttpPost]
        public IActionResult UpdatePhoto(int id, FeaturedWorkComponentUpdatePhotoVM model)
        {
            if (!ModelState.IsValid) return View();

            var featuredWorkComponentPhoto = _context.FeaturedWorkComponentPhotos.Find(id);
            if (featuredWorkComponentPhoto is null) return NotFound();

            featuredWorkComponentPhoto.Order = model.Order;

            _context.FeaturedWorkComponentPhotos.Update(featuredWorkComponentPhoto);
            _context.SaveChanges();

            return RedirectToAction(nameof(Details), "featuredworkcomponent", new { id = featuredWorkComponentPhoto.FeaturedWorkComponentId });
        }
        #endregion
    }
}

