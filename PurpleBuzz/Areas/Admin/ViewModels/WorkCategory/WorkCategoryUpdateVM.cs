using System;
using System.ComponentModel.DataAnnotations;

namespace PurpleBuzz.Areas.Admin.ViewModels.WorkCategory
{
	public class WorkCategoryUpdateVM
	{
        [Required(ErrorMessage = "Ad mütləq daxil edilməlidir")]
        [MinLength(3, ErrorMessage = "Minimum uzunluq 3 olmalıdır")]
        public string Name { get; set; }
	}
}

