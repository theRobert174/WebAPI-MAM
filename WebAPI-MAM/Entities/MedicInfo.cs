using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI_MAM.Entities
{
    public class MedicInfo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(16)]
        public string nss { get; set; }

        [Required]
        public double weight { get; set; }

        [Required]
        public double height { get; set; }

        public string sicknessHistory { get; set; }

        //[ForeignKey("patient")]    
        public int patientId { get; set; }
        public Patients patient { get; set; } //No agregar REQUIRED, sino desde PatrientController Post retorna BadRequest
    }
}
