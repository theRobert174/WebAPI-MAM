using System.ComponentModel.DataAnnotations;


namespace WebAPI_MAM.DTO_s.Update
{
    public class UpDoctorDTO
    {
        [Required]
        [EmailAddress]
        public string Mail { get; set; }

        [Required]
        public string password { get; set; }
    }

}
