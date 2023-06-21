using System;
using PurpleBuzz.Models;

namespace PurpleBuzz.Areas.Admin.ViewModels.FeaturedWorkComponent
{
	public class FeaturedWorkComponentDetailsVM
	{
		public string Description { get; set; }
		public DateTime CreateAt { get; set; }
		public DateTime? ModifiedAt { get; set; }
		public List<FeaturedWorkComponentPhoto> Photos { get; set; }
	}
}

