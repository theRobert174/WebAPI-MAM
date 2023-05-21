using System.ComponentModel.DataAnnotations;

namespace WebAPI_MAM.DTO_s.Set
{
    public class DiagnosisDTO
    {
        [Required]
        public string observations { get; set; }

        [Required]
        public string diagnostic { get; set; }

        [Required]
        public string treatment { get; set; }

        [Required]
        public string drugs { get; set; }

        //Puede cambiar,el "?" lo puse para que al momento de hacer un diagnostico, no pida una cita inmediatamente
        public int/*?*/ appointmentId { get; set; }
    }
}
