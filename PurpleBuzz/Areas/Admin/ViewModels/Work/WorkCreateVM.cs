using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PurpleBuzz.Areas.Admin.ViewModels.Work
{
	public class WorkCreateVM
	{
		[Required]
		[MinLength(3)]
		[MaxLength(10)]
		public string Title { get; set; }

		[Required]
		[MinLength(5)]
		public string Description { get; set; }
		public string PhotoPath { get; set; }

        [Display(Name = "Work Category")]
        public int WorkCategoryId { get; set; }
		public List<SelectListItem>? WorkCategories { get; set; }
	}
}

