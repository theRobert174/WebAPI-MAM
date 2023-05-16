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

        public int appointmentId { get; set; }
    }
}
