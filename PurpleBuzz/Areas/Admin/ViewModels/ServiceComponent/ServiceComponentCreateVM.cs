using System;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace PurpleBuzz.Areas.Admin.ViewModels.ServiceComponent
{
	public class ServiceComponentCreateVM
	{
		[Required(ErrorMessage = "Başlıq daxil olmalıdır")]
		[MaxLength(20,ErrorMessage = "Başlıq 20 simvol olmalıdır")]
		public string Title { get; set; }

        [MaxLength(20, ErrorMessage = "Başlıq 20 simvol olmalıdır")]
        public string Subtitle { get; set; }

		[Required(ErrorMessage = "Başlıq daxil olmalıdır")]
		[MinLength(10)]
		[MaxLength(100, ErrorMessage ="Açıqlama maksimum 100 simvol olmalıdır")]

		public string Description { get; set; }
	}
}

