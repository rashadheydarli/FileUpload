using System.ComponentModel.DataAnnotations;

namespace PurpleBuzz.ViewModels.User
{
    public class UserCreateVM
    {
        [MaxLength(10, ErrorMessage = "Adın uzunluğu maksimum 10 olmalıdır")]
        public string Name { get; set; }

        [Required]
        [MaxLength(10, ErrorMessage = "AÇıqlamanın uzunluğu maksimum 100 olmalıdır")]
        public string Surname { get; set; }
    }
}
