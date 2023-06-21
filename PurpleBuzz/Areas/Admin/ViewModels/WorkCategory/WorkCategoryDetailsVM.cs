using System;
namespace PurpleBuzz.Areas.Admin.ViewModels.WorkCategory
{
	public class WorkCategoryDetailsVM
	{
		public string Name { get; set; }
		public bool IsDeleted{ get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime? ModifiedDate { get; set; }
		public DateTime? DeletedDate { get; set; }
		public List<Models.Work> Works { get; set; }

	}
}

