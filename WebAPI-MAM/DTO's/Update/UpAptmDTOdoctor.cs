using System.ComponentModel.DataAnnotations;

namespace WebAPI_MAM.DTO_s.Update
{
    public class UpAptmDTOdoctor
    {
        [Required]
        public int doctorId { get; set; }
    }
}
