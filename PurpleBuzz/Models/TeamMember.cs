using System;
namespace PurpleBuzz.Models
{
	public class TeamMember
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string  Surname { get; set; }
		public string About { get; set; }
		public string Duty { get; set; }
		public string PhotoName { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? ModifiedAt { get; set; }
	}
}

