using System.ComponentModel.DataAnnotations;

namespace WebAPI_MAM.DTO_s.Set
{
    public class DoctorDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Mail { get; set; }

        [Required]
        public string password { get; set; }
    }
}
