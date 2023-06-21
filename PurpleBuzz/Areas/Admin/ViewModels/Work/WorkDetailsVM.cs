using System;
namespace PurpleBuzz.Areas.Admin.ViewModels.Work
{
	public class WorkDetailsVM
	{
        public string Title { get; set; }
        public string Description { get; set; }
        public string PhotoPath { get; set; }
        public int WorkCategoryId { get; set; }
        public Models.WorkCategory WorkCategory { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}

