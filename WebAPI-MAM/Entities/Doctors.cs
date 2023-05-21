using System.ComponentModel.DataAnnotations;
using WebAPI_MAM.Validators;

namespace WebAPI_MAM.Entities
{
    public class Doctors
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Mail { get; set; }

        [Required]
       // [FirstLetterUppercase]
        //[PasswordValidation]
        public string password { get; set; }

        public List<Appointments> appointments { get; set; }
    }
}
