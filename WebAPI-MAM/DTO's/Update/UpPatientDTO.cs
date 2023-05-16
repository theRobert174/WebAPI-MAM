using System.ComponentModel.DataAnnotations;
using WebAPI_MAM.DTO_s.Set;

namespace WebAPI_MAM.DTO_s.Update
{
    public class UpPatientDTO
    {
        [Required]
        public string name { get; set; }

        [Required]
        [EmailAddress]
        public string mail { get; set; }

        [Required]
        [Phone]
        public string phone { get; set; }

        [Required]
        [Phone]
        public string cel { get; set; }

        [Required(ErrorMessage = "Datos medicos del paciente no establecidos")]
        public MedicInfoDTO medicInfo { get; set; }
    }
}
