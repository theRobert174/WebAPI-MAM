using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI_MAM.Entities
{
    public class Diagnosis
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string observations { get; set; }

        [Required]
        public string diagnostic { get; set; }

        [Required]
        public string treatment { get; set; }

        [Required]
        public string drugs { get; set; }

        public Appointments appointment { get; set; }//No agregar REQUIRED, sino desde AptmController Post retorna BadRequest
    }
}
