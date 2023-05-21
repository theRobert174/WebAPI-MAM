using System.ComponentModel.DataAnnotations;

namespace WebAPI_MAM.DTO_s.Set
{
    public class CredencialsUser
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
