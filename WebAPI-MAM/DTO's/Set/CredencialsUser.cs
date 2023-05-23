using System.ComponentModel.DataAnnotations;
using WebAPI_MAM.Validators;

namespace WebAPI_MAM.DTO_s.Set
{
    public class CredencialsUser
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        [Required]
        [DoctorValidation]
        public string Role { get; set; }
    }
}
