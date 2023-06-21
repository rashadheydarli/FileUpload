using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PurpleBuzz.Areas.Admin.ViewModels.TeamMember;
using PurpleBuzz.DAL;
using PurpleBuzz.Models;
using PurpleBuzz.Utilities;

namespace PurpleBuzz.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/team-member/{action=list}/{id?}")]
    public class TeamMemberController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AppDbContext _context;
        private readonly IFileService _fileService;
        public TeamMemberController(IWebHostEnvironment webHostEnvironment, AppDbContext context, IFileService fileService)
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;
            _fileService = fileService;
        }
        
        public IActionResult List()
        {
            var model = new TeamMemberListVM
            {
                TeamMembers = _context.TeamMembers.ToList()
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TeamMemberCreateVM model)
        {
            if (!ModelState.IsValid) return View();

            if (!_fileService.IsImage(model.Photo))
            {
                ModelState.AddModelError("Photo", "Fayl düzgün formatda deyil");
                return View();

            }

            if(_fileService.IsBiggerThanSize(model.Photo, 200))
            {
                ModelState.AddModelError("Photo", "Şəkilin ölçüsü 200kb dan çoxdur");
                return View();
            }
            
            var teamMember = new TeamMember
            {
                Name = model.Name,
                Surname = model.Surname,
                About = model.About,
                Duty = model.Duty,
                CreatedAt = DateTime.Now,
                PhotoName = _fileService.Upload(model.Photo)
            };
            _context.TeamMembers.Add(teamMember);
            _context.SaveChanges();

            return RedirectToAction("list");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var teamMember = _context.TeamMembers.Find(id);
            if (teamMember is null) return NotFound();

            var model = new TeamMemberUpdateVM
            {
                Name = teamMember.Name,
                Surname =teamMember.Surname,
                Duty = teamMember.Duty,
                About = teamMember.About,
                PhotoName = teamMember.PhotoName

            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Update(int id,TeamMemberUpdateVM model)
        {
            if (!ModelState.IsValid) return View();

            var teamMember = _context.TeamMembers.Find(id);
            if (teamMember is null) return NotFound();
            


            if(model.Photo is not null)
            {
                if (!_fileService.IsImage(model.Photo))
                {
                    ModelState.AddModelError("Photo", "Fayl düzgün formatda deyil");
                    return View();

                }

                if (_fileService.IsBiggerThanSize(model.Photo, 200))
                {
                    ModelState.AddModelError("Photo", "Şəkilin ölçüsü 200kb dan çoxdur");
                    return View();
                }
                _fileService.Delete(teamMember.PhotoName);
                teamMember.PhotoName = _fileService.Upload(model.Photo);
            }

            teamMember.Name = model.Name;
            teamMember.Surname = model.Surname;
            teamMember.Duty = model.Duty;
            teamMember.About = model.About;
            teamMember.ModifiedAt = DateTime.Now;

            _context.TeamMembers.Update(teamMember);
            _context.SaveChanges();

            return RedirectToAction(nameof(List));
        }

            



        [HttpGet]
        public IActionResult Delete(int id)
        {
            var teamMember = _context.TeamMembers.Find(id);
            if (teamMember is null) return NotFound();

            _fileService.Delete(teamMember.PhotoName);

            _context.TeamMembers.Remove(teamMember);
            _context.SaveChanges();

            return RedirectToAction("list");
        }
    }
}

