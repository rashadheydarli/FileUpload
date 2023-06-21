using System;
namespace PurpleBuzz.Models
{
	public class FeaturedWorkComponent
	{
		public int Id { get; set; }
		public string Description{ get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime? ModifiedDate { get; set; }
		public ICollection<FeaturedWorkComponentPhoto> Photos { get; set; }

	}
}

