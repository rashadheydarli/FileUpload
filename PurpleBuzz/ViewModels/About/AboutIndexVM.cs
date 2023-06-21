using PurpleBuzz.Models;

namespace PurpleBuzz.ViewModels.About
{
    public class AboutIndexVM
    {
        public AboutIntroComponent AboutIntroComponent { get; set; }

        public WhyYouChoose WhyYouChoose { get; set; }

        public List<Our> Ours { get; set; }

        public List<TeamMember> TeamMembers { get; set; }
    }
}
