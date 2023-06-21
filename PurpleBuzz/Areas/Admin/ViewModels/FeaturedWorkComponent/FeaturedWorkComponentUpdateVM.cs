using System;
using PurpleBuzz.Models;

namespace PurpleBuzz.Areas.Admin.ViewModels.FeaturedWorkComponent
{
	public class FeaturedWorkComponentUpdateVM
	{
        public FeaturedWorkComponentUpdateVM()
        {
            Photos = new List<IFormFile>();
        }
        public string Description { get; set; }
        public List<FeaturedWorkComponentPhoto>? FeaturedWorkComponentPhotos{ get; set; }  //menim movcud olan sekillerimdi
        public List<IFormFile>? Photos { get; set; }  // yeni yukleyeceyim sekiller ucundu

    }
}

