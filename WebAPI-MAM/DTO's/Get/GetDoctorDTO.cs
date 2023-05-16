using System.ComponentModel.DataAnnotations;

namespace WebAPI_MAM.DTO_s.Get
{
    public class GetDoctorDTO //Información más relevante para el usuario paciente (el id del doc, el nombre y su forma de contacto)
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Mail { get; set; }
    }
}
