using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace PurpleBuzz.Areas.Admin.ViewModels.Work
{
	public class WorkUpdateVM
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

