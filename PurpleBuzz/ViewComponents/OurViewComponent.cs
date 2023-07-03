using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using PurpleBuzz.DAL;

namespace PurpleBuzz.ViewComponents
{
	public class OurViewComponent :ViewComponent
	{
		private readonly AppDbContext _context;
		public OurViewComponent(AppDbContext context)
		{
			_context = context;
		}
		public IViewComponentResult Invoke()
		{
			var ours = _context.Ours.Where(o => !o.IsDeleted).ToList();
			return View(ours);
		}
	}
}

