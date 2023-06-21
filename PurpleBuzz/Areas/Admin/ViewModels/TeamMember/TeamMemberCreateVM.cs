using System;
using System.ComponentModel.DataAnnotations;

namespace PurpleBuzz.Areas.Admin.ViewModels.TeamMember
{
	public class TeamMemberCreateVM
	{
		[Required]
		public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Duty { get; set; }

		[MaxLength(100)]
		public string About { get; set; }

        [Required]
        public IFormFile Photo { get; set; }
	}
}

