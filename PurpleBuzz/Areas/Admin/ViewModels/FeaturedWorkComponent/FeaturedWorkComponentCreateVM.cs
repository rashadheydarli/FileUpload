using System;
using System.ComponentModel.DataAnnotations;

namespace PurpleBuzz.Areas.Admin.ViewModels.FeaturedWorkComponent
{
	public class FeaturedWorkComponentCreateVM
	{
		public FeaturedWorkComponentCreateVM()
		{
			Photos = new List<IFormFile>();
		}

		[Required]
		public string Description { get; set; }

		[Required]
		public List<IFormFile> Photos { get; set; }
	}
}

