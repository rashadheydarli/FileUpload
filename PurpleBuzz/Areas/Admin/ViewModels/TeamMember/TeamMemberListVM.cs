using System;
namespace PurpleBuzz.Areas.Admin.ViewModels.TeamMember
{
	public class TeamMemberListVM
	{
		public TeamMemberListVM()
		{
			TeamMembers = new List<Models.TeamMember>();
		}
		public List<Models.TeamMember> TeamMembers { get; set; }
	}
}

