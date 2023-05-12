using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI_MAM.Entities
{
    public class Patients
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        [EmailAddress]
        public string mail { get; set; }

        [Required]
        public string password { get; set; }

        [Required]
        [Phone]
        public string phone { get; set; }

        [Required]
        [Phone]
        public string cel { get; set; }

        public List<Appointments> appointments { get; set; }

        [Required(ErrorMessage = "Datos medicos del paciente no establecidos")]
        [ForeignKey("medicInfo")]
        public int medicInfoId { get; set; }
        public MedicInfo medicInfo { get; set; }


    }
}
