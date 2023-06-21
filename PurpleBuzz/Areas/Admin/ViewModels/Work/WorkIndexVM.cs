using System;
namespace PurpleBuzz.Areas.Admin.ViewModels.Work
{
	public class WorkIndexVM
	{
		public WorkIndexVM()
		{
			Works = new List<Models.Work>();
		}
		public List<Models.Work> Works { get; set; }
		
	}
}

