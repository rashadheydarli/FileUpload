using PurpleBuzz.Models;

namespace PurpleBuzz.ViewModels.Home
{
    public class HomeIndexVM
    {
        public HomeIndexVM()
        {
            Works = new List<Models.Work>();
        }

        public string WorksTitle { get; set; }
        public List<Models.Work> Works { get; set; }

        public ServiceComponent ServiceComponent { get; set; }
    }
}
