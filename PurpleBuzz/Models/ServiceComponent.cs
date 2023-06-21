using System;
namespace PurpleBuzz.Models
{
	public class ServiceComponent
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Subtitle { get; set; }
		public string Description { get; set; }
		public bool IsDeleted { get; set; }
	}
}

