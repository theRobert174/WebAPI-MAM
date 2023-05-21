using System.ComponentModel.DataAnnotations;

namespace WebAPI_MAM.DTO_s.Update
{
    public class UpAdminDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
