using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPI_MAM.Validators;

namespace WebAPI_MAM.Entities
{
    public class Appointments
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [ApStatus]
        public string Status { get; set; }

        [Required]
        [ForeignKey("doctor")]
        public int doctorId { get; set; }
        public Doctors doctor { get; set; }

        [Required]
        [ForeignKey("patient")]
        public int patientId { get; set; }
        public Patients patient { get; set; }

        [ForeignKey("diagnostic")]
        public int diagnosticId { get; set; }
        public Diagnosis diagnostic { get; set; }
    }
}
